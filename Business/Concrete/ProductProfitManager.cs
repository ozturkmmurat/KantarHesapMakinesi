using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductProfitManager : IProductProfitService
    {
        IProductProfitDal _productProfitDal;
        public ProductProfitManager(IProductProfitDal productProfitDal)
        {
            _productProfitDal = productProfitDal;
        }
        public IResult Add(ProductProfit productProfit)
        {
            if (productProfit != null)
            {
                _productProfitDal.Add(productProfit);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<ProductProfit> GetByProductId(int productId)
        {
            var result = _productProfitDal.Get(x => x.ProductId == productId);
            if (result != null)
            {
                return new SuccessDataResult<ProductProfit>(result);
            }
            return new ErrorDataResult<ProductProfit>();
        }

        public IResult Update(ProductProfit productProfit)
        {
            if (productProfit != null)
            {
                _productProfitDal.Update(productProfit);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
