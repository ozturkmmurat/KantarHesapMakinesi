using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        IProductProfitService _productProfitService;
        public ProductManager(IProductDal productDal, IProductProfitService productProfitService)
        {
            _productDal = productDal;
            _productProfitService=productProfitService;
        }
        [SecuredOperation("admin")]
        public IResult Add(Product product)
        {
            if (product != null)
            {
                _productDal.Add(product);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(Product product)
        {
            if (product != null)
            {
                _productDal.Delete(product);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        public IDataResult<List<Product>> GetAllProduct()
        {
            var result = _productDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Product>>(result, Messages.GetByAll);
            }
            return new ErrorDataResult<List<Product>>(result, Messages.GetByAllDefault);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<ProductDto>> GetAllProductDto()
        {
            var result = _productDal.GetAllProductDto();
            if (result != null)
            {
                return new SuccessDataResult<List<ProductDto>>(result);
            }
            return new ErrorDataResult<List<ProductDto>>();
        }
        [SecuredOperation("admin")]
        public IDataResult<Product> GetById(int id)
        {
            var result = _productDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Product>(result);
            }
            return new ErrorDataResult<Product>();
        }
        [SecuredOperation("admin")]
        public IResult TsaAdd(CRUDProductDto crudProductDto)
        {
            if (crudProductDto != null)
            {
                Product product = new Product();
                product.ProductName = crudProductDto.ProductName;
                var productAddResult = Add(product);

                if(productAddResult.Success)
                {
                    ProductProfit productProfit = new ProductProfit();
                    productProfit.ProductId = product.Id;
                    productProfit.ProfitPercentage = crudProductDto.ProfitPercentage;
                    productProfit.AdditionalProfitPercentage = crudProductDto.AdditionalProfitPercentage;
                    var productProfitResult = _productProfitService.Add(productProfit);

                    if (productProfitResult.Success)
                    {
                        return new SuccessResult();
                    }
                }
            }
            return new ErrorResult();
        }

        [SecuredOperation("admin")]
        [TransactionScopeAspect]
        public IResult TsaUpdate(CRUDProductDto crudProductDto)
        {
            if (crudProductDto != null)
            {
                Product product = new Product();
                product.Id = crudProductDto.ProductId;
                product.ProductName = crudProductDto.ProductName;
                var productUpdateResult = Update(product);

                ProductProfit productProfit = new ProductProfit();
                productProfit.Id = crudProductDto.ProductProfitId;
                productProfit.ProductId = product.Id;
                productProfit.ProfitPercentage = crudProductDto.ProfitPercentage;
                productProfit.AdditionalProfitPercentage = crudProductDto.AdditionalProfitPercentage;
                var productProfitResult = _productProfitService.Update(productProfit);

                if (productProfitResult.Success && productUpdateResult.Success)
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult();
        }

        [SecuredOperation("admin")]
        public IResult Update(Product product)
        {
            if (product != null)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
