using Business.Abstract;
using Business.Abstract.ProductModelCost;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract.ProductModelCost;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.ProductModelCost
{
    [LogAspect(typeof(FileLogger))]
    public class ProductModelCostDetailManager : IProductModelCostDetailService
    {
        IProductModelCostDetailDal _productModelCostDetailDal;
        IProductModelCostService _productModelCostService;
        IInstallationCostService _installationCostService;
        IAccessoryService _accessoryService;
        public ProductModelCostDetailManager(
            IProductModelCostDetailDal productModelCostDetailDal,
            IProductModelCostService productModelCostService,
            IInstallationCostService installationCostService,
            IAccessoryService accessoryService)
        {
            _productModelCostDetailDal = productModelCostDetailDal;
            _productModelCostService = productModelCostService;
            _installationCostService = installationCostService;
            _accessoryService = accessoryService;
        }

        public Entities.Concrete.ProductModelCostDetail CostCalculateDetail(ProductModelCostDetail productModelCostDetail)
        {
            var productModelCost = _productModelCostService.GetById(productModelCostDetail.ModelId);
            var accessoryResult = _accessoryService.GetById(productModelCostDetail.AccessoryId);
            var installationCostResult = _installationCostService.GetInstallationCostByLocationId(productModelCostDetail.InstallationCostLocationId);

            if (accessoryResult.Success) //Hesaplama işlemi için bir aksesuar seçilmiş mi ?
            {
                productModelCostDetail.AccessoryTlPrice = accessoryResult.Data.AccessoryTlPrice;
                productModelCostDetail.AccessoryEuroPrice = accessoryResult.Data.AccessoryEuroPrice;
            }
            else
            {
                productModelCostDetail.AccessoryTlPrice = 0;
                productModelCostDetail.AccessoryEuroPrice = 0;
            }

            if (installationCostResult.Success) //Kurulum yeri seçilmiş mi ?
            {

                productModelCostDetail.InstallationIncludedTl += productModelCost.Data.OverheadIncluded + productModelCost.Data.ElectronicTlAmount + productModelCostDetail.AccessoryTlPrice
                       + installationCostResult.Data.InstallationTlPrice;
                productModelCostDetail.InstallationIncludedEuro = TCMBCalculation.TLEuroCalculation(productModelCostDetail.InstallationIncludedTl);
                productModelCostDetail.SalesPriceTl += productModelCostDetail.InstallationIncludedTl;
                productModelCostDetail.SalesPriceEuro += productModelCostDetail.InstallationIncludedEuro;
            }
            else
            {
                productModelCostDetail.InstallationIncludedTl += productModelCost.Data.OverheadIncluded + productModelCost.Data.ElectronicTlAmount + productModelCostDetail.AccessoryTlPrice
                       + 0;
                productModelCostDetail.InstallationIncludedEuro = TCMBCalculation.TLEuroCalculation(productModelCostDetail.InstallationIncludedTl);
                productModelCostDetail.SalesPriceTl += productModelCostDetail.InstallationIncludedTl;
                productModelCostDetail.SalesPriceEuro += TCMBCalculation.TLEuroCalculation(productModelCostDetail.SalesPriceTl);
            }

            

            var salePriceKeep = productModelCostDetail.SalesPriceTl;
            productModelCostDetail.TurkeySalesPrice +=salePriceKeep;
            productModelCostDetail.ExportSalesPrice += productModelCostDetail.SalesPriceEuro;
            productModelCostDetail.ProfitPriceTl += productModelCostDetail.TurkeySalesPrice * productModelCost.Data.ProfitPercentage / 100;
            productModelCostDetail.ProfitPriceEuro = productModelCostDetail.ExportSalesPrice * productModelCost.Data.ProfitPercentage / 100;
            productModelCostDetail.TurkeySalesDiscountPrice += (productModelCostDetail.ProfitPriceTl + productModelCostDetail.TurkeySalesPrice);
            productModelCostDetail.ExportFinalDiscountPrice += (productModelCostDetail.ProfitPriceEuro + productModelCostDetail.ExportSalesPrice);
            productModelCostDetail.OfferPriceTl +=  productModelCostDetail.TurkeySalesDiscountPrice + (productModelCostDetail.TurkeySalesDiscountPrice * productModelCost.Data.AdditionalProfitPercentage /100);
            productModelCostDetail.OfferPriceEuro += productModelCostDetail.ExportFinalDiscountPrice + (productModelCostDetail.ExportFinalDiscountPrice * productModelCost.Data.AdditionalProfitPercentage / 100);

            return productModelCostDetail;
        }

        public IDataResult<ProductModelCostDetail> GetCalculate(int modelId, int installationCostLocationId, int accessoryId)
        {
            ProductModelCostDetail productModelCostDetail = new ProductModelCostDetail()
            {
                ModelId = modelId,
               InstallationCostLocationId = installationCostLocationId,
               AccessoryId = accessoryId,
            };
            var result = CostCalculateDetail(productModelCostDetail);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDetail>(result);
            }
            return new ErrorDataResult<ProductModelCostDetail>(Messages.GetByAllDefault);
        }

    }
}
