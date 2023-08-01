using Business.Abstract;
using Business.Abstract.ProductModelCost;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.Constans.Variables;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.ExchangeRate.CurrencyGet;
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
        IModelService _modelService;
        public ProductModelCostDetailManager(
            IProductModelCostDetailDal productModelCostDetailDal,
            IProductModelCostService productModelCostService,
            IInstallationCostService installationCostService,
            IAccessoryService accessoryService,
            IModelService modelService)
        {
            _productModelCostDetailDal = productModelCostDetailDal;
            _productModelCostService = productModelCostService;
            _installationCostService = installationCostService;
            _accessoryService = accessoryService;
            _modelService = modelService;
        }

        public Entities.Concrete.ProductModelCostDetail CostCalculateDetail(ProductModelCostDetail productModelCostDetail)
        {
            var getModel = _modelService.GetById(productModelCostDetail.ModelId);
            var productModelCost = _productModelCostService.GetByModelIdCurrency(productModelCostDetail.ModelId, productModelCostDetail.CurrencyName);
            var accessoryResult = _accessoryService.GetById(productModelCostDetail.AccessoryId);
            var installationCostResult = _installationCostService.GetInstallationCostByLocationId(productModelCostDetail.InstallationCostLocationId);

            if (productModelCostDetail.ExportState)
            {
                if (accessoryResult.Success) //Hesaplama işlemi için bir aksesuar seçilmiş mi ?
                {
                    productModelCostDetail.AccessoryPrice = accessoryResult.Data.AccessoryEuroPrice;
                }
                else
                {
                    productModelCostDetail.AccessoryPrice = 0;
                }
                if (installationCostResult.Success) //Kurulum yeri seçilmiş mi ?
                {

                    productModelCostDetail.InstallationIncluded += TCMBCalculation.DivideCurrencyCalculation(productModelCost.Data.OverheadIncluded +productModelCost.Data.ElectronicAmount
                     +  installationCostResult.Data.InstallationTlPrice, CurrencyGet.ForexBuyingCurrencyGet("EUR")) + productModelCostDetail.AccessoryPrice;
                    productModelCostDetail.SalesPrice += productModelCostDetail.InstallationIncluded;
                }
                var salePriceKeep = productModelCostDetail.SalesPrice;
                productModelCostDetail.ProfitPrice = productModelCostDetail.SalesPrice * getModel.Data.ProfitPercentage / 100;
                productModelCostDetail.FinalDiscountPrice += (productModelCostDetail.ProfitPrice + productModelCostDetail.SalesPrice);
                productModelCostDetail.OfferPrice += productModelCostDetail.FinalDiscountPrice + (productModelCostDetail.FinalDiscountPrice * getModel.Data.AdditionalProfitPercentage / 100);
            }
            else if(productModelCostDetail.CurrencyName != "TRY" && !productModelCostDetail.ExportState) 
            {
                productModelCostDetail.InstallationIncluded += productModelCost.Data.OverheadIncluded + productModelCost.Data.ElectronicAmount
                    + productModelCostDetail.AccessoryPrice + 0;
                productModelCostDetail.SalesPrice += productModelCostDetail.InstallationIncluded;
                var salePriceKeep = productModelCostDetail.SalesPrice;
                productModelCostDetail.ProfitPrice = productModelCostDetail.SalesPrice * getModel.Data.ProfitPercentage / 100;
                productModelCostDetail.FinalDiscountPrice += (productModelCostDetail.ProfitPrice + productModelCostDetail.SalesPrice);
                productModelCostDetail.OfferPrice += productModelCostDetail.FinalDiscountPrice + (productModelCostDetail.FinalDiscountPrice * getModel.Data.AdditionalProfitPercentage / 100);
            }
          

            return productModelCostDetail;
        }

        public IDataResult<ProductModelCostDetail> GetCalculate(ProductModelCostDetail productModelCostDetail)
        {
            var result = CostCalculateDetail(productModelCostDetail);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDetail>(result);
            }
            return new ErrorDataResult<ProductModelCostDetail>(Messages.GetByAllDefault);
        }

    }
}
