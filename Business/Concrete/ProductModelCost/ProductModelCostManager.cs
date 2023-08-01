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
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Business.Constans.Variables;
using Core.Utilities.ExchangeRate.CurrencyGet;

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
                if (GetByModelIdCurrency(productModelCost.ModelId, productModelCost.CurrencyName).Data != null)  //Eğer belirtilen modele ait parametreden gelen döviz türünde maliyet hesaplması var ise ekleme işlemi yapma
                {
                    return new ErrorResult();
                }
                else
                {
                    _productModelCostDal.Add(productModelCost);
                    return new SuccessResult(Messages.DataAdded);
                }
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        //Belli bir modelId sahip maliyet oluştururken kullanılıyor
        [SecuredOperation("admin")]
        public IResult AddProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null && GetById(productModelCostDto.ModelId).Success == false )
            {
                var result = CostCalculate(productModelCostDto);
                for (int i = 0; i < result.Data.Count; i++)
                {
                    Add(MappingProductModelCost(result.Data[i]).Data);
                }
                return new SuccessResult(Messages.DataAdded);
            }
            else
            {
                return new ErrorResult("Bu model için zaten mevcut bir maliyet var ");
            }
        }
        //Belli bir modelıd'ye göre maliyet hesaplarken kullanılıyor.
        [SecuredOperation("admin")]
        public IDataResult<List<ProductModelCostDto>> CostCalculate(ProductModelCostDto productModelCostDto)
        {
            List<ProductModelCostDto> productModelCostDtos = new List<ProductModelCostDto>();
            //Kantarın maliyet hesaplaması yapılıyor model ile productModelCostId birebir ilişkili 

            var modelResult = _modelService.GetById(productModelCostDto.ModelId);
            var costVariable = _costVariableService.GetById(modelResult.Data.CostVariableId); 
            var size = _sizeService.GetById(modelResult.Data.SizeId); 
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
                            newProductModelCostDto.ShateIronPrice = TCMBCalculation.CurrencyCalculation(modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron, calculateCurrency);
                            newProductModelCostDto.IProfilePrice = TCMBCalculation.CurrencyCalculation(modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile, calculateCurrency);
                            newProductModelCostDto.MaterialAmount += newProductModelCostDto.ShateIronPrice + newProductModelCostDto.IProfilePrice;
                            newProductModelCostDto.TotalLaborCost += TCMBCalculation.CurrencyCalculation(costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime, calculateCurrency);
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
                            newProductModelCostDto.ShateIronPrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron, iCurrency, calculateCurrency);
                            newProductModelCostDto.IProfilePrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile, iCurrency, calculateCurrency);
                            newProductModelCostDto.MaterialAmount += newProductModelCostDto.ShateIronPrice + newProductModelCostDto.IProfilePrice;
                            newProductModelCostDto.TotalLaborCost += TCMBCalculation.EuroBasedCurrencyCalculate(costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime, iCurrency, calculateCurrency);
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
                        newProductModelCostDto.ShateIronPrice = modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron;
                        newProductModelCostDto.IProfilePrice = modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile;
                        newProductModelCostDto.MaterialAmount += newProductModelCostDto.ShateIronPrice + newProductModelCostDto.IProfilePrice;
                        newProductModelCostDto.TotalLaborCost += costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime;
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
                var modelResult = _modelService.GetById(productModelCostDtos[i].ModelId);
                var costVariable = _costVariableService.GetById(modelResult.Data.CostVariableId);
                var size = _sizeService.GetById(modelResult.Data.SizeId);
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
                                productModelCostDtos[i].ShateIronPrice = TCMBCalculation.CurrencyCalculation(modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron, calculateCurrency);
                                productModelCostDtos[i].IProfilePrice = TCMBCalculation.CurrencyCalculation(modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile, calculateCurrency);
                                productModelCostDtos[i].MaterialAmount += productModelCostDtos[i].ShateIronPrice + productModelCostDtos[i].IProfilePrice;
                                productModelCostDtos[i].TotalLaborCost += TCMBCalculation.CurrencyCalculation(costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime, calculateCurrency);
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
                                productModelCostDtos[i].ShateIronPrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron, iCurrency, calculateCurrency);
                                productModelCostDtos[i].IProfilePrice = TCMBCalculation.EuroBasedCurrencyCalculate(modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile, iCurrency, calculateCurrency);
                                productModelCostDtos[i].MaterialAmount += productModelCostDtos[i].ShateIronPrice + productModelCostDtos[i].IProfilePrice;
                                productModelCostDtos[i].TotalLaborCost += TCMBCalculation.EuroBasedCurrencyCalculate(costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime, iCurrency, calculateCurrency);
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
                            productModelCostDtos[i].ShateIronPrice = modelResult.Data.FireShateIronWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.ShateIron;
                            productModelCostDtos[i].IProfilePrice = modelResult.Data.FireIProfileWeight * _costVariableService.GetById(modelResult.Data.CostVariableId).Data.IProfile;
                            productModelCostDtos[i].MaterialAmount += productModelCostDtos[i].ShateIronPrice + productModelCostDtos[i].IProfilePrice;
                            productModelCostDtos[i].TotalLaborCost += costVariable.Data.LaborCostPerHourEuro * modelResult.Data.ProductionTime;
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
        public IResult Delete(Entities.Concrete.ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                _productModelCostDal.Delete(productModelCost);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }

        public IDataResult<List<ProductModelCostDto>> GetAllDto()
        {
            var result = _productModelCostDal.GetAllDto();
            if (result != null)
            {
                return new SuccessDataResult<List<ProductModelCostDto>>(result);
            }
            return new ErrorDataResult<List<ProductModelCostDto>>();
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

        public IDataResult<Entities.Concrete.ProductModelCost> GetByModelIdCurrency(int modelId, string currencyName)
        {
            var result = _productModelCostDal.Get(x => x.ModelId == modelId && x.CurrencyName == currencyName);
            if (result != null)
            {
                return new SuccessDataResult<Entities.Concrete.ProductModelCost>(result);
            }
            return new ErrorDataResult<Entities.Concrete.ProductModelCost>();
        }

        //[SecuredOperation("admin")]
        public IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelIdCurrency(int modelId, string currencyName)
        {
            var result = _productModelCostDal.GetProductModelCostDtoById(x => x.ModelId == modelId && x.CurrencyName == currencyName);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDto>(result);
            }
            return new ErrorDataResult<ProductModelCostDto>();
        }
        //Belli bir modele göre güncelleme işleminde mapleme
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
        //Toplu güncelleme de Mapleme işlemi
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

        [SecuredOperation("admin")]
        public IResult Update(Entities.Concrete.ProductModelCost productModelCost)
        {
            if (productModelCost != null)
            {
                if (GetByModelIdCurrency(productModelCost.ModelId, productModelCost.CurrencyName).Data == null)
                {
                    _productModelCostDal.Add(productModelCost);
                }
                _productModelCostDal.Update(productModelCost);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
        //Belirli bir modelId sahip verileri güncelleme
        [SecuredOperation("admin")]
        public IResult UpdateProductModelCost(ProductModelCostDto productModelCostDto)
        {
            if (productModelCostDto != null)
            {
                var result = CostCalculate(productModelCostDto);
                for (int i = 0; i < result.Data.Count; i++)
                {
                    var getModel = GetByModelIdCurrency(productModelCostDto.ModelId, result.Data[i].CurrencyName);
                    if (result.Data[i].ModelId == getModel.Data.ModelId && result.Data[i].CurrencyName == getModel.Data.CurrencyName)
                    {
                        result.Data[i].ProductModelCostId = getModel.Data.Id;
                        Update(MappingProductModelCost(result.Data[i]).Data);
                    }
                }
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
        //Toplu Güncelleme işlemi
        [SecuredOperation("admin")]
        public IResult UpdateRange(List<ProductModelCostDto> productModelCostDtos)
        {
            var result = CostCalculateList(productModelCostDtos);
            
            if (result != null)
            {
                var x = MappingProductModelCostList(result.Data);
                _productModelCostDal.UpdateRange(x.Data);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
