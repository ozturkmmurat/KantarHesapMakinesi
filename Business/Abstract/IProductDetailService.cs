using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductDetailService
    {
        IDataResult<List<ProductDetailDto>> GetAllProductDto();
        IDataResult<ProductDetail> GetById(int id);
        IResult Add(ProductDetail productDetail);
        IResult Update(ProductDetail productDetail);
        IResult Delete(ProductDetail productDetail);
    }
}
