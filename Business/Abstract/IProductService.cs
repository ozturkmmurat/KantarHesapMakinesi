using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAllProduct();
        IDataResult<List<ProductDto>> GetAllProductDto();
        IDataResult<Product> GetById(int id);
        IResult Add(Product product);
        IResult TsaAdd(CRUDProductDto crudProductDto);
        IResult Update(Product product);
        IResult Delete(Product product);
        IResult TsaUpdate(CRUDProductDto crudProductDto);
    }
}
