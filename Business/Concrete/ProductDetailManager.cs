using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
    public class ProductDetailManager : IProductDetailService
    {
        IProductDetailDal _productDetailDal;
        public ProductDetailManager(IProductDetailDal productDetailDal)
        {
            _productDetailDal = productDetailDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(ProductDetail productDetail)
        {
            if (productDetail != null)
            {
                _productDetailDal.Add(productDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(ProductDetail productDetail)
        {
            if (productDetail != null)
            {
                _productDetailDal.Delete(productDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<ProductDetailDto>> GetAllProductDto()
        {
            var result = _productDetailDal.GetAllProductDto();
            if (result.Count != null)
            {
                return new SuccessDataResult<List<ProductDetailDto>>(result);
            }
            return new ErrorDataResult<List<ProductDetailDto>>(result);
        }

        public IDataResult<ProductDetail> GetById(int id)
        {
            var result = _productDetailDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<ProductDetail>(result);
            }
            return new ErrorDataResult<ProductDetail>(result);
        }
        [SecuredOperation("admin")]
        public IResult Update(ProductDetail productDetail)
        {
            if (productDetail != null)
            {
                _productDetailDal.Update(productDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
