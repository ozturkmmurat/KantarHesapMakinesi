using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductModelCostDetailDal : IEntityRepository<Entities.Concrete.ProductModelCostDetail>
    {
        List<ProductModelCostDetailDto> GetAllProductModelCostDetailDtos(Expression<Func<ProductModelCostDetailDto,bool>> filter = null);
        ProductModelCostDetailDto GetByIdProductModelCostDetailDto(Expression<Func<ProductModelCostDetailDto, bool>> filter);
    }
}
