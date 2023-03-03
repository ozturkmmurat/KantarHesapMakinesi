using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.Dtos;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Aspects.Autofac.Transaction;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class ProductModelCostManager : IProductModelCostService
    {
        IProductModelCostDal _productModelCostDal;
        IModelService _modelService; 
        IModelAccessoryDetailService _modelAccessoryDetailService;
        IModelElectronicDetailService _modelElectronicDetailService;
        ICostVariableService _costVariableService;
        IInstallationCostService _installationCostService;
        IProductModelCostDetailService _productModelCostDetailService;
        IAccessoryPackageDetailService _accessoryPackageDetailService;
        public ProductModelCostManager(
            IProductModelCostDal productModelCostDal,
            IModelService modelService,
            IModelAccessoryDetailService modelAccessoryDetailService,
            IModelElectronicDetailService modelElectronicDetailService,
            ICostVariableService costVariableService,
            IInstallationCostService installationCostService,
            IProductModelCostDetailService productModelCostDetailService,
            IAccessoryPackageDetailService accessoryPackageDetailService)
        {
            _productModelCostDal = productModelCostDal;
            _modelService = modelService;
            _modelAccessoryDetailService = modelAccessoryDetailService;
            _modelElectronicDetailService = modelElectronicDetailService;
            _costVariableService = costVariableService;
            _installationCostService = installationCostService;
            _productModelCostDetailService = productModelCostDetailService;
            _accessoryPackageDetailService = accessoryPackageDetailService;
        }
        [SecuredOperation("admin")]
        public IResult Add(ProductModelCost productModelCost)
        {
            _productModelCostDal.Add(productModelCost);
            return new SuccessResult();
        }
        [SecuredOperation("admin")]
        [TransactionScopeAspect]
        public IResult AddProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null && GetById(productModelCostDto.ModelId).Success == false  )
            {
                var result = CostCalculate(productModelCostDto);
                var mappingProductModelCost = MappingProductModelCost(productModelCostDto);
                Add(mappingProductModelCost.Data);
                _productModelCostDetailService.Add(MappingProductModelCostDetail(productModelCostDto).Data);
                return new SuccessResult();
            }
            else
            {
                
                 var result = CostCalculate(productModelCostDto);
                if(_productModelCostDetailService.GetProductModelCostDetailLocationModelId(productModelCostDto.LocationId, productModelCostDto.ModelId).Success == true)
                {
                    _productModelCostDetailService.Add(MappingProductModelCostDetail(result).Data);
                    return new SuccessResult();
                }
                return new ErrorResult("Bu bölge için kurulum maliyeti zaten mevcut.");
            }
            return new ErrorResult();
        }

        public ProductModelCostDto CostCalculate(ProductModelCostDto productModelCostDto)
        {
            var modelResult = _modelService.GetById(productModelCostDto.ModelId);
            var electronicResult = _modelElectronicDetailService.GetAllModelElectronicDetailDtoByModelId(productModelCostDto.ModelId);
            var modelAccesoryDetailByIdResult = _modelAccessoryDetailService.GetModelAccesoryDetailDtoByModelId(productModelCostDto.ModelId);
            var installationCostResult = _installationCostService.GetInstallationCostByLocationId(productModelCostDto.InstallationCostLocationId);
            var accessoryPackageDetailResult = _accessoryPackageDetailService.GetAllAccessoryPackageDtoById(modelAccesoryDetailByIdResult.Data.AccessoryPackageId);

            productModelCostDto.ProductModelCostId = productModelCostDto.ModelId;
            productModelCostDto.InstallationCostId = installationCostResult.Data.InstallationCostId;
            if (productModelCostDto != null) 
            {

                foreach (var item in electronicResult.Data)
                {
                    productModelCostDto.ProductModelCostElectronicTlAmount += TCMBCalculation.EuroCalculation(item.ElectronicEuroPrice * item.ModelElectronicDetailsElectronicPcs);
                    productModelCostDto.ProductModelCostElectronicEuroAmount += item.ElectronicEuroPrice;
                }

                foreach (var item in accessoryPackageDetailResult.Data)
                {
                    productModelCostDto.ProductModelCostAccessoriesTlAmount += TCMBCalculation.EuroCalculation(item.AccessoryEuroPrice * item.AccessoryPackageDetailAccessoryPcs);
                    productModelCostDto.ProductModelCostAccessoriesEuroAmount += item.AccessoryEuroPrice;
                }

                productModelCostDto.ProductModelCostShateIronEuroPrice = modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron;
                productModelCostDto.ProductModelCostIProfileEuroPrice = modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile;

                productModelCostDto.ProductModelCostMaterialEuroAmount += productModelCostDto.ProductModelCostShateIronEuroPrice + productModelCostDto.ProductModelCostIProfileEuroPrice;
                productModelCostDto.ProductModelCostMaterialTlAmount += TCMBCalculation.EuroCalculation(productModelCostDto.ProductModelCostMaterialEuroAmount);

                productModelCostDto.ProductModelCostTotalLaborCost += productModelCostDto.ProductModelCostLaborCostPerHour * modelResult.Data.ProductionTime;
                productModelCostDto.ProductModelCostTotalAmount += productModelCostDto.ProductModelCostTotalLaborCost + productModelCostDto.ProductModelCostMaterialTlAmount;

                var totalAmountKeep = productModelCostDto.ProductModelCostTotalAmount;
                productModelCostDto.ProductModelCostGeneralExpenseAmount += totalAmountKeep * productModelCostDto.ProductModelCostOverheadPercentage / 100;
                productModelCostDto.ProductModelCostOverheadIncluded += totalAmountKeep += productModelCostDto.ProductModelCostGeneralExpenseAmount;
                productModelCostDto.ProductModelCostDetailProductModelCostId = productModelCostDto.ProductModelCostId;
                productModelCostDto.ProductModelCostDetailInstallationIncluded += productModelCostDto.ProductModelCostOverheadIncluded + productModelCostDto.ProductModelCostElectronicTlAmount + productModelCostDto.ProductModelCostAccessoriesTlAmount 
                    + installationCostResult.Data.InstallationTlPrice;
                productModelCostDto.ProductModelCostDetailSalesPrice += productModelCostDto.ProductModelCostDetailInstallationIncluded;
                var salePriceKeep = productModelCostDto.ProductModelCostDetailSalesPrice;
                productModelCostDto.ProductModelCostDetailTurkeySalesPrice += TCMBCalculation.TLEuroCalculation(salePriceKeep);
                productModelCostDto.ProductModelCostDetailProfitPrice += productModelCostDto.ProductModelCostDetailTurkeySalesPrice * productModelCostDto.ProductModelCostDetailProfitPercentage / 100;
                productModelCostDto.ProductModelCostDetailTurkeySalesDiscountPrice += (productModelCostDto.ProductModelCostDetailProfitPrice + productModelCostDto.ProductModelCostDetailTurkeySalesPrice) - productModelCostDto.ProductModelCostDetailTurkeySalesDiscount;
                productModelCostDto.ProductModelCostDetailExportFinalDiscountPrice += (productModelCostDto.ProductModelCostDetailProfitPrice + productModelCostDto.ProductModelCostDetailTurkeySalesPrice) - productModelCostDto.ProductModelCostDetailExportFinalDiscount;
                return productModelCostDto;
            }
            else
            {
                throw new Exception("Böyle bir veri bulunmakta.");
            }
            return null;
        }
        [SecuredOperation("admin")]
        public IResult Delete(ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                _productModelCostDal.Delete(productModelCost);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<ProductModelCost>> GetAllProductModelCost()
        {
            var result = _productModelCostDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<ProductModelCost>>(result);
            }
            return new ErrorDataResult<List<ProductModelCost>>();
        }

        public IDataResult<ProductModelCost> GetById(int id)
        {
            var result = _productModelCostDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCost>(result);
            }
            return new ErrorDataResult<ProductModelCost>();
        }

        public IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelId(int modelId)
        {
            var result = _productModelCostDal.GetProductModelCostDtoById(x => x.ModelId == modelId);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDto>(result);
            }
            return new ErrorDataResult<ProductModelCostDto>();
        }

        public IDataResult<ProductModelCost> MappingProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null)
            {
                ProductModelCost productModelCost = new ProductModelCost()
                {
                    Id = productModelCostDto.ProductModelCostId,
                    ShateIronEuroPrice = productModelCostDto.ProductModelCostShateIronEuroPrice,
                    IProfileEuroPrice = productModelCostDto.ProductModelCostIProfileEuroPrice,
                    MaterialTlAmount = productModelCostDto.ProductModelCostMaterialTlAmount,
                    MaterialEuroAmount = productModelCostDto.ProductModelCostMaterialEuroAmount,
                    LaborCostPerHour = productModelCostDto.ProductModelCostLaborCostPerHour,
                    TotalLaborCost = productModelCostDto.ProductModelCostTotalLaborCost,
                    TotalAmount = productModelCostDto.ProductModelCostTotalAmount,
                    OverheadPercentage = productModelCostDto.ProductModelCostOverheadPercentage,
                    GeneralExpenseAmount = productModelCostDto.ProductModelCostGeneralExpenseAmount,
                    OverheadIncluded = productModelCostDto.ProductModelCostOverheadIncluded,
                    ElectronicTlAmount = productModelCostDto.ProductModelCostElectronicTlAmount,
                    ElectronicEuroAmount = productModelCostDto.ProductModelCostElectronicEuroAmount,
                    AccessioresEuroAmount = productModelCostDto.ProductModelCostAccessoriesEuroAmount,
                    AccessoriesTlAmount = productModelCostDto.ProductModelCostAccessoriesTlAmount,
                };
                return new SuccessDataResult<ProductModelCost>(productModelCost);
            }
            return new ErrorDataResult<ProductModelCost>();
        }

        public IDataResult<Entities.Concrete.ProductModelCostDetail> MappingProductModelCostDetail(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null)
            {
                ProductModelCostDetail productModelCostDetail = new ProductModelCostDetail()
                {
                    Id = productModelCostDto.ProductModelCostDetailId,
                    InstallationCostId = productModelCostDto.InstallationCostId,
                    ProductModelCostId = productModelCostDto.ProductModelCostDetailProductModelCostId,
                    InstallationIncluded = productModelCostDto.ProductModelCostDetailInstallationIncluded,
                    SalesPrice = productModelCostDto.ProductModelCostDetailSalesPrice,
                    TurkeySalesPrice = productModelCostDto.ProductModelCostDetailTurkeySalesPrice,
                    ProfitPrice = productModelCostDto.ProductModelCostDetailProfitPrice,
                    TurkeySalesDiscountPrice = productModelCostDto.ProductModelCostDetailTurkeySalesDiscountPrice, // İncele
                    ExportFinalDiscountPrice = productModelCostDto.ProductModelCostDetailExportFinalDiscountPrice,
                    ProfitPercentage = productModelCostDto.ProductModelCostDetailProfitPercentage, // İncele
                    ExportFinalDiscount = productModelCostDto.ProductModelCostDetailExportFinalDiscount,
                    TurkeySalesDiscount = productModelCostDto.ProductModelCostDetailTurkeySalesDiscount
                };
                return new SuccessDataResult<ProductModelCostDetail>(productModelCostDetail);
            }
            return new ErrorDataResult<ProductModelCostDetail>();
        }
        [SecuredOperation("admin")]
        public IResult Update(ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                _productModelCostDal.Update(productModelCost);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [TransactionScopeAspect]
        [SecuredOperation("admin")]
        public IResult UpdateProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null)
            {
                var mappingProductModelCost = MappingProductModelCost(CostCalculate(productModelCostDto));
                Update(mappingProductModelCost.Data);
               
                _productModelCostDetailService.Update(MappingProductModelCostDetail(productModelCostDto).Data);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
