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
using Business.Constans;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.ProductModelCost;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class ProductModelCostManager : IProductModelCostService
    {
        IProductModelCostDal _productModelCostDal;
        IModelService _modelService; 
        ICostVariableService _costVariableService;
        IInstallationCostService _installationCostService;
        ISizeService _sizeService;
        ISizeContentService _sizeContentService;
        public ProductModelCostManager(
            IProductModelCostDal productModelCostDal,
            IModelService modelService,
            ICostVariableService costVariableService,
            IInstallationCostService installationCostService,
            ISizeService sizeService,
            ISizeContentService sizeContentService)
        {
            _productModelCostDal = productModelCostDal;
            _modelService = modelService;
            _costVariableService = costVariableService;
            _installationCostService = installationCostService;
            _sizeService = sizeService;
            _sizeContentService = sizeContentService;
        }
        [SecuredOperation("admin")] //SecuredOperation Yetkilendirme için giriş yapan kullanıcının admin yetkisi var ise bu işlemi sağlayabiliyor
        public IResult Add(Entities.Concrete.ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                _productModelCostDal.Add(productModelCost);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult AddProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null && GetById(productModelCostDto.ModelId).Success == false )
            {
                var result = CostCalculate(productModelCostDto);
                Add(MappingProductModelCost(result).Data);
                return new SuccessResult(Messages.DataAdded);
            }
            else
            {
                return new ErrorResult("Bu model için zaten mevcut bir maliyet var ");
            }
        }
        [SecuredOperation("admin")]
        public ProductModelCostDto CostCalculate(ProductModelCostDto productModelCostDto)
        {

            //Kantarın maliyet hesaplaması yapılıyor model ile productModelCostId birebir ilişkili 

            var modelResult = _modelService.GetById(productModelCostDto.ModelId);
            var costVariable = _costVariableService.GetById(modelResult.Data.CostVariableId); 
            var size = _sizeService.GetById(modelResult.Data.SizeId); 
            var sizeContent = _sizeContentService.GetAllSizeCtDtoBySizeId(size.Data.Id);

            productModelCostDto.ProductModelCostId = productModelCostDto.ModelId;
            if (productModelCostDto != null) 
            {
                foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                {
                    productModelCostDto.ProductModelCostElectronicTlAmount += TCMBCalculation.EuroCalculation(item.ElectronicEuroPrice * item.ElectronicPcs);  
                    productModelCostDto.ProductModelCostElectronicEuroAmount += item.ElectronicEuroPrice;
                }

                productModelCostDto.ProductModelCostShateIronEuroPrice = modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron;
                productModelCostDto.ProductModelCostIProfileEuroPrice = modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile;

                productModelCostDto.ProductModelCostMaterialEuroAmount += productModelCostDto.ProductModelCostShateIronEuroPrice + productModelCostDto.ProductModelCostIProfileEuroPrice;
                productModelCostDto.ProductModelCostMaterialTlAmount += TCMBCalculation.EuroCalculation(productModelCostDto.ProductModelCostMaterialEuroAmount);

                productModelCostDto.ProductModelCostTotalLaborCostEuro += costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime;
                productModelCostDto.ProductModelCostTotalLaborCostTl += TCMBCalculation.EuroCalculation(productModelCostDto.ProductModelCostTotalLaborCostEuro);
                productModelCostDto.ProductModelCostTotalAmount += productModelCostDto.ProductModelCostTotalLaborCostTl + productModelCostDto.ProductModelCostMaterialTlAmount;

                var totalAmountKeep = productModelCostDto.ProductModelCostTotalAmount;
                productModelCostDto.ProductModelCostGeneralExpenseAmount += totalAmountKeep * costVariable.Data.OverheadPercentage / 100;
                productModelCostDto.ProductModelCostOverheadIncluded += totalAmountKeep += productModelCostDto.ProductModelCostGeneralExpenseAmount;
                return productModelCostDto;
            }
            else
            {
                throw new Exception("Böyle bir veri bulunmakta.");
            }
            return null;
        }


        [SecuredOperation("admin")]
        public IResult Delete(Entities.Concrete.ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                _productModelCostDal.Delete(productModelCost);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<Entities.Concrete.ProductModelCost>> GetAllProductModelCost()
        {
            var result = _productModelCostDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Entities.Concrete.ProductModelCost>>(result);
            }
            return new ErrorDataResult<List<Entities.Concrete.ProductModelCost>>();
        }
        public IDataResult<Entities.Concrete.ProductModelCost> GetById(int id)
        {
            var result = _productModelCostDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Entities.Concrete.ProductModelCost>(result);
            }
            return new ErrorDataResult<Entities.Concrete.ProductModelCost>();
        }

        [SecuredOperation("admin")]
        public IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelId(int modelId)
        {
            var result = _productModelCostDal.GetProductModelCostDtoById(x => x.ModelId == modelId);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDto>(result);
            }
            return new ErrorDataResult<ProductModelCostDto>();
        }

        [SecuredOperation("admin")]
        public IDataResult<Entities.Concrete.ProductModelCost> MappingProductModelCost(ProductModelCostDto productModelCostDto)
        {
            //ProductModelCost için birden fazla yerde aynı map işlemi kullanıldığı için oluşturuldu.
            if (productModelCostDto != null)
            {
                Entities.Concrete.ProductModelCost productModelCost = new Entities.Concrete.ProductModelCost()
                {
                    Id = productModelCostDto.ProductModelCostId,
                    ShateIronEuroPrice = productModelCostDto.ProductModelCostShateIronEuroPrice,
                    IProfileEuroPrice = productModelCostDto.ProductModelCostIProfileEuroPrice,
                    MaterialTlAmount = productModelCostDto.ProductModelCostMaterialTlAmount,
                    MaterialEuroAmount = productModelCostDto.ProductModelCostMaterialEuroAmount,
                    TotalLaborCostTl = productModelCostDto.ProductModelCostTotalLaborCostTl,
                    TotalLaborCostEuro = productModelCostDto.ProductModelCostTotalLaborCostEuro,
                    TotalAmount = productModelCostDto.ProductModelCostTotalAmount,
                    GeneralExpenseAmount = productModelCostDto.ProductModelCostGeneralExpenseAmount,
                    OverheadIncluded = productModelCostDto.ProductModelCostOverheadIncluded,
                    ElectronicTlAmount = productModelCostDto.ProductModelCostElectronicTlAmount,
                    ElectronicEuroAmount = productModelCostDto.ProductModelCostElectronicEuroAmount,
                    ProfitPercentage = productModelCostDto.ProductModelCostProfitPercentage,
                    AdditionalProfitPercentage = productModelCostDto.ProductModelCostAdditionalProfitPercentage
                    
                };
                return new SuccessDataResult<Entities.Concrete.ProductModelCost>(productModelCost);
            }
            return new ErrorDataResult<Entities.Concrete.ProductModelCost>();
        }


        [SecuredOperation("admin")]
        public IResult Update(Entities.Concrete.ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                _productModelCostDal.Update(productModelCost);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }

        [SecuredOperation("admin")]
        public IResult UpdateProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null)
            {
                var result = CostCalculate(productModelCostDto);
                Update(MappingProductModelCost(result).Data);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
