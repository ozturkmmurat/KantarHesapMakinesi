using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDetailDal : IEntityRepository<ProductDetail>
    {
        List<ProductDetailDto> GetAllProductDto(Expression<Func<ProductDetailDto, bool>> filter = null);
    }
}
