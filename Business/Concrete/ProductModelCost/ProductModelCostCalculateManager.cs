using Business.Abstract;
using Business.Abstract.ProductModelCost;
using Business.BusinessAspects.Autofac;
using Business.Constans.Variables;
using Business.Constans;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Utilities.ExchangeRate.CurrencyGet;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete.ProductModelCost
{
    public class ProductModelCostCalculateManager : IProductModelCostCalculateService
    {
        //Belli bir modelId sahip maliyet oluştururken kullanılıyor
        IModelDal _modelDal;
        ICostVariableService _costVariableService;
        ISizeService _sizeService;
        ISizeContentService _sizeContentService;

        public ProductModelCostCalculateManager(
            IModelDal modelDal,
            ICostVariableService costVariableService,
            ISizeService sizeService,
            ISizeContentService sizeContentService)
        {
            _modelDal = modelDal;
            _costVariableService = costVariableService;
            _sizeService = sizeService;
            _sizeContentService = sizeContentService;
        }


        [SecuredOperation("admin")]
        public IDataResult<List<Entities.Concrete.ProductModelCost>> AddProductModelCost(ProductModelCostDto productModelCostDto)
        {

            var result = CostCalculate(productModelCostDto);
            var mapResult = MappingProductModelCostList(result.Data);
            return new SuccessDataResult<List<Entities.Concrete.ProductModelCost>>(mapResult.Data);
        }
        //Belli bir modelıd'ye göre maliyet hesaplarken kullanılıyor.
        [SecuredOperation("admin")]
        public IDataResult<List<ProductModelCostDto>> CostCalculate(ProductModelCostDto productModelCostDto)
        {
            List<ProductModelCostDto> productModelCostDtos = new List<ProductModelCostDto>();
            //Kantarın maliyet hesaplaması yapılıyor model ile productModelCostId birebir ilişkili 

            var modelResult = _modelDal.Get(x => x.Id == productModelCostDto.ModelId);
            var costVariable = _costVariableService.GetById(modelResult.CostVariableId);
            var size = _sizeService.GetById(modelResult.SizeId);
            var sizeContent = _sizeContentService.GetAllSizeCtDtoBySizeId(size.Data.Id);
            var calculateCurrency = CurrencyGet.ForexBuyingCurrencyGet("EUR");
            decimal iCurrency = 0;

            if (calculateCurrency == 0)
            {
                throw new Exception("o");
            }

            for (int i = 0; i < Variable.CurrencyArray.Length; i++)
            {
                if (productModelCostDto != null)
                {
                    var newProductModelCostDto = new ProductModelCostDto()
                    {
                        ModelId = productModelCostDto.ModelId
                    };
                    if (Variable.CurrencyArray[i] != "EUR")
                    {

                        newProductModelCostDto.CurrencyName = Variable.CurrencyArray[i];
                        if (Variable.CurrencyArray[i] == "TRY")
                        {
                            foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                            {
                                newProductModelCostDto.ElectronicAmount += TCMBCalculation.CurrencyCalculation(item.ElectronicEuroPrice * item.ElectronicPcs, CurrencyGet.ForexBuyingCurrencyGet("EUR"));
                            }
                            newProductModelCostDto.ShateIronPrice = TCMBCalculation.CurrencyCalculation(modelResult.FireShateIronWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.ShateIron, calculateCurrency);
                            newProductModelCostDto.IProfilePrice = TCMBCalculation.CurrencyCalculation(modelResult.FireIProfileWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.IProfile, calculateCurrency);
                            newProductModelCostDto.MaterialAmount += newProductModelCostDto.ShateIronPrice + newProductModelCostDto.IProfilePrice;
                            newProductModelCostDto.TotalLaborCost += TCMBCalculation.CurrencyCalculation(costVariable.Data.LaborCostPerHourEuro * modelResult.ProductionTime, calculateCurrency);
                            newProductModelCostDto.TotalAmount += newProductModelCostDto.TotalLaborCost + newProductModelCostDto.MaterialAmount;
                            var totalAmountKeeppp = newProductModelCostDto.TotalAmount;
                            newProductModelCostDto.GeneralExpenseAmount += totalAmountKeeppp * costVariable.Data.OverheadPercentage / 100;
                            newProductModelCostDto.OverheadIncluded += totalAmountKeeppp += newProductModelCostDto.GeneralExpenseAmount;
                            newProductModelCostDto.ProfitPercentage = productModelCostDto.ProfitPercentage;
                            newProductModelCostDto.AdditionalProfitPercentage = productModelCostDto.AdditionalProfitPercentage;
                        }
                        else
                        {
                            iCurrency = CurrencyGet.ForexBuyingCurrencyGet(Variable.CurrencyArray[i]);
                            foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                            {
                                newProductModelCostDto.ElectronicAmount += TCMBCalculation.EuroBasedCurrencyCalculate((item.ElectronicEuroPrice * item.ElectronicPcs), iCurrency, calculateCurrency);
                            }
                            newProductModelCostDto.ShateIronPrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.FireShateIronWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.ShateIron, iCurrency, calculateCurrency);
                            newProductModelCostDto.IProfilePrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.FireIProfileWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.IProfile, iCurrency, calculateCurrency);
                            newProductModelCostDto.MaterialAmount += newProductModelCostDto.ShateIronPrice + newProductModelCostDto.IProfilePrice;
                            newProductModelCostDto.TotalLaborCost += TCMBCalculation.EuroBasedCurrencyCalculate(costVariable.Data.LaborCostPerHourEuro * modelResult.ProductionTime, iCurrency, calculateCurrency);
                            newProductModelCostDto.TotalAmount += newProductModelCostDto.TotalLaborCost + newProductModelCostDto.MaterialAmount;
                            var totalAmountKeep = newProductModelCostDto.TotalAmount;
                            newProductModelCostDto.GeneralExpenseAmount += totalAmountKeep * costVariable.Data.OverheadPercentage / 100;
                            newProductModelCostDto.OverheadIncluded += totalAmountKeep += newProductModelCostDto.GeneralExpenseAmount;
                            newProductModelCostDto.ProfitPercentage = productModelCostDto.ProfitPercentage;
                            newProductModelCostDto.AdditionalProfitPercentage = productModelCostDto.AdditionalProfitPercentage;
                        }

                    }
                    else
                    {
                        foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                        {
                            newProductModelCostDto.ElectronicAmount += item.ElectronicEuroPrice * item.ElectronicPcs;
                        }
                        newProductModelCostDto.CurrencyName = Variable.CurrencyArray[i];
                        newProductModelCostDto.ShateIronPrice = modelResult.FireShateIronWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.ShateIron;
                        newProductModelCostDto.IProfilePrice = modelResult.FireIProfileWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.IProfile;
                        newProductModelCostDto.MaterialAmount += newProductModelCostDto.ShateIronPrice + newProductModelCostDto.IProfilePrice;
                        newProductModelCostDto.TotalLaborCost += costVariable.Data.LaborCostPerHourEuro * modelResult.ProductionTime;
                        newProductModelCostDto.TotalAmount += newProductModelCostDto.TotalLaborCost + newProductModelCostDto.MaterialAmount;
                        var totalAmountKeepp = newProductModelCostDto.TotalAmount;
                        newProductModelCostDto.GeneralExpenseAmount += totalAmountKeepp * costVariable.Data.OverheadPercentage / 100;
                        newProductModelCostDto.OverheadIncluded += totalAmountKeepp += newProductModelCostDto.GeneralExpenseAmount;
                        newProductModelCostDto.ProfitPercentage = productModelCostDto.ProfitPercentage;
                        newProductModelCostDto.AdditionalProfitPercentage = productModelCostDto.AdditionalProfitPercentage;
                    }
                    productModelCostDtos.Add(newProductModelCostDto);
                }

                else
                {
                    throw new Exception("Böyle bir veri bulunmakta.");
                }

            }
            return new SuccessDataResult<List<ProductModelCostDto>>(productModelCostDtos);
        }
        //Toplu bir güncelleme de maliyeti hesaplama da kullanılıyor
        public IDataResult<List<ProductModelCostDto>> CostCalculateList(List<ProductModelCostDto> productModelCostDtos)
        {
            var calculateCurrency = CurrencyGet.ForexBuyingCurrencyGet("EUR");
            decimal iCurrency = 0;
            for (int i = 0; i < productModelCostDtos.Count; i++)
            {
                var modelResult = _modelDal.Get(x => x.Id == productModelCostDtos[i].ModelId);
                var costVariable = _costVariableService.GetById(modelResult.CostVariableId);
                var size = _sizeService.GetById(modelResult.SizeId);
                var sizeContent = _sizeContentService.GetAllSizeCtDtoBySizeId(size.Data.Id);

                productModelCostDtos[i].ProductModelCostId = productModelCostDtos[i].ProductModelCostId;

                if (productModelCostDtos[i] != null)
                {
                    if (productModelCostDtos[i].CurrencyName != "EUR") // Patladı
                    {
                        if (productModelCostDtos[i].CurrencyName == "TRY")
                        {
                            foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                            {
                                productModelCostDtos[i].ElectronicAmount += TCMBCalculation.CurrencyCalculation(item.ElectronicEuroPrice * item.ElectronicPcs, calculateCurrency);
                            }
                            productModelCostDtos[i].ShateIronPrice = TCMBCalculation.CurrencyCalculation(modelResult.FireShateIronWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.ShateIron, calculateCurrency);
                            productModelCostDtos[i].IProfilePrice = TCMBCalculation.CurrencyCalculation(modelResult.FireIProfileWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.IProfile, calculateCurrency);
                            productModelCostDtos[i].MaterialAmount += productModelCostDtos[i].ShateIronPrice + productModelCostDtos[i].IProfilePrice;
                            productModelCostDtos[i].TotalLaborCost += TCMBCalculation.CurrencyCalculation(costVariable.Data.LaborCostPerHourEuro * modelResult.ProductionTime, calculateCurrency);
                            productModelCostDtos[i].TotalAmount += productModelCostDtos[i].TotalLaborCost + productModelCostDtos[i].MaterialAmount;
                            var totalAmountKeeppp = productModelCostDtos[i].TotalAmount;
                            productModelCostDtos[i].GeneralExpenseAmount += totalAmountKeeppp * costVariable.Data.OverheadPercentage / 100;
                            productModelCostDtos[i].OverheadIncluded += totalAmountKeeppp += productModelCostDtos[i].GeneralExpenseAmount;
                            productModelCostDtos[i].ProfitPercentage = productModelCostDtos[i].ProfitPercentage;
                            productModelCostDtos[i].AdditionalProfitPercentage = productModelCostDtos[i].AdditionalProfitPercentage;
                        }
                        else
                        {
                            iCurrency = CurrencyGet.ForexBuyingCurrencyGet(productModelCostDtos[i].CurrencyName);
                            foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                            {
                                productModelCostDtos[i].ElectronicAmount += TCMBCalculation.EuroBasedCurrencyCalculate((item.ElectronicEuroPrice * item.ElectronicPcs), iCurrency, calculateCurrency);
                            }
                            productModelCostDtos[i].ShateIronPrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.FireShateIronWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.ShateIron, iCurrency, calculateCurrency);
                            productModelCostDtos[i].IProfilePrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.FireIProfileWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.IProfile, iCurrency, calculateCurrency);
                            productModelCostDtos[i].MaterialAmount += productModelCostDtos[i].ShateIronPrice + productModelCostDtos[i].IProfilePrice;
                            productModelCostDtos[i].TotalLaborCost += TCMBCalculation.EuroBasedCurrencyCalculate(costVariable.Data.LaborCostPerHourEuro * modelResult.ProductionTime, iCurrency, calculateCurrency);
                            productModelCostDtos[i].TotalAmount += productModelCostDtos[i].TotalLaborCost + productModelCostDtos[i].MaterialAmount;
                            var totalAmountKeep = productModelCostDtos[i].TotalAmount;
                            productModelCostDtos[i].GeneralExpenseAmount += totalAmountKeep * costVariable.Data.OverheadPercentage / 100;
                            productModelCostDtos[i].OverheadIncluded += totalAmountKeep += productModelCostDtos[i].GeneralExpenseAmount;
                            productModelCostDtos[i].ProfitPercentage = productModelCostDtos[i].ProfitPercentage;
                            productModelCostDtos[i].AdditionalProfitPercentage = productModelCostDtos[i].AdditionalProfitPercentage;
                        }

                    }
                    else
                    {
                        foreach (var item in sizeContent.Data)  //Modelin sahip olduğu En Boya kayıtlı olan elektronikleri dönüyoruz.
                        {
                            productModelCostDtos[i].ElectronicAmount += item.ElectronicEuroPrice * item.ElectronicPcs;
                        }
                        productModelCostDtos[i].CurrencyName = productModelCostDtos[i].CurrencyName;
                        productModelCostDtos[i].ShateIronPrice = modelResult.FireShateIronWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.ShateIron;
                        productModelCostDtos[i].IProfilePrice = modelResult.FireIProfileWeight * _costVariableService.GetById(modelResult.CostVariableId).Data.IProfile;
                        productModelCostDtos[i].MaterialAmount += productModelCostDtos[i].ShateIronPrice + productModelCostDtos[i].IProfilePrice;
                        productModelCostDtos[i].TotalLaborCost += costVariable.Data.LaborCostPerHourEuro * modelResult.ProductionTime;
                        productModelCostDtos[i].TotalAmount += productModelCostDtos[i].TotalLaborCost + productModelCostDtos[i].MaterialAmount;
                        var totalAmountKeepp = productModelCostDtos[i].TotalAmount;
                        productModelCostDtos[i].GeneralExpenseAmount += totalAmountKeepp * costVariable.Data.OverheadPercentage / 100;
                        productModelCostDtos[i].OverheadIncluded += totalAmountKeepp += productModelCostDtos[i].GeneralExpenseAmount;
                        productModelCostDtos[i].ProfitPercentage = productModelCostDtos[i].ProfitPercentage;
                        productModelCostDtos[i].AdditionalProfitPercentage = productModelCostDtos[i].AdditionalProfitPercentage;
                    }
                }

                else
                {
                    throw new Exception("Böyle bir veri bulunmakta.");
                }
            }
            return new SuccessDataResult<List<ProductModelCostDto>>(productModelCostDtos);
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
                    ModelId = productModelCostDto.ModelId,
                    CurrencyName = productModelCostDto.CurrencyName,
                    ShateIronPrice = productModelCostDto.ShateIronPrice,
                    IProfilePrice = productModelCostDto.IProfilePrice,
                    MaterialAmount = productModelCostDto.MaterialAmount,
                    TotalLaborCost = productModelCostDto.TotalLaborCost,
                    TotalAmount = productModelCostDto.TotalAmount,
                    GeneralExpenseAmount = productModelCostDto.GeneralExpenseAmount,
                    OverheadIncluded = productModelCostDto.OverheadIncluded,
                    ElectronicAmount = productModelCostDto.ElectronicAmount

                };
                return new SuccessDataResult<Entities.Concrete.ProductModelCost>(productModelCost);
            }
            return new ErrorDataResult<Entities.Concrete.ProductModelCost>();
        }

        public IDataResult<List<Entities.Concrete.ProductModelCost>> MappingProductModelCostList(List<ProductModelCostDto> productModelCostDtos)
        {
            if (productModelCostDtos != null)
            {
                List<Entities.Concrete.ProductModelCost> productModelCostList = new List<Entities.Concrete.ProductModelCost>();
                for (int i = 0; i < productModelCostDtos.Count; i++)
                {
                    Entities.Concrete.ProductModelCost productModelCost = new Entities.Concrete.ProductModelCost()
                    {
                        Id = productModelCostDtos[i].ProductModelCostId,
                        ModelId = productModelCostDtos[i].ModelId,
                        CurrencyName = productModelCostDtos[i].CurrencyName,
                        ShateIronPrice = productModelCostDtos[i].ShateIronPrice,
                        IProfilePrice = productModelCostDtos[i].IProfilePrice,
                        MaterialAmount = productModelCostDtos[i].MaterialAmount,
                        TotalLaborCost = productModelCostDtos[i].TotalLaborCost,
                        TotalAmount = productModelCostDtos[i].TotalAmount,
                        GeneralExpenseAmount = productModelCostDtos[i].GeneralExpenseAmount,
                        OverheadIncluded = productModelCostDtos[i].OverheadIncluded,
                        ElectronicAmount = productModelCostDtos[i].ElectronicAmount
                    };
                    productModelCostList.Add(productModelCost);
                }
                return new SuccessDataResult<List<Entities.Concrete.ProductModelCost>>(productModelCostList);
            }
            return new ErrorDataResult<List<Entities.Concrete.ProductModelCost>>();
        }

        public IDataResult<List<ProductModelCostDto>> UpdateProductModelCost(ProductModelCostDto productModelCostDto)
        {
            var result = CostCalculate(productModelCostDto);
            return new SuccessDataResult<List<ProductModelCostDto>>(result.Data);
        }
    }
}
