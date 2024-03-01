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
using Business.Abstract.ProductModelCost;
using System.Collections;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class ProductModelCostManager : IProductModelCostService
    {
        IProductModelCostCalculateService _addProductModelCostService;
        IProductModelCostDal _productModelCostDal;
        public ProductModelCostManager(
            IProductModelCostDal productModelCostDal,
            IProductModelCostCalculateService addProductModelCostService)
        {
            _productModelCostDal = productModelCostDal;
            _addProductModelCostService = addProductModelCostService;
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
            if (productModelCostDto != null && GetById(productModelCostDto.ModelId).Success == false)
            {
                var result = _addProductModelCostService.AddProductModelCost(productModelCostDto);
                if (result.Success)
                {
                    _productModelCostDal.AddRange(result.Data);
                    return new SuccessResult(Messages.DataAdded);
                }
            }
            return new ErrorResult("Bu model için zaten mevcut bir maliyet var ");
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

        public IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelIdCurrency(int modelId, string currencyName)
        {
            var result = _productModelCostDal.GetProductModelCostDtoById(x => x.ModelId == modelId && x.CurrencyName == currencyName);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDto>(result);
            }
            return new ErrorDataResult<ProductModelCostDto>();
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
                var result = _addProductModelCostService.UpdateProductModelCost(productModelCostDto);
                for (int i = 0; i < result.Data.Count; i++)
                {
                    var getModel = GetByModelIdCurrency(productModelCostDto.ModelId, result.Data[i].CurrencyName);
                    if (result.Data[i].ModelId == getModel.Data.ModelId && result.Data[i].CurrencyName == getModel.Data.CurrencyName)
                    {
                        result.Data[i].ProductModelCostId = getModel.Data.Id;
                        Update(_addProductModelCostService.MappingProductModelCost(result.Data[i]).Data);
                    }
                }
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
        // Bu metod sadece BackgroundService de kullaniliyor
        public IResult UpdateRange(List<ProductModelCostDto> productModelCostDtos)
        {
            var result = _addProductModelCostService.CostCalculateList(productModelCostDtos);
            
            if (result != null)
            {
                var x = _addProductModelCostService.MappingProductModelCostList(result.Data);
                _productModelCostDal.UpdateRange(x.Data);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
