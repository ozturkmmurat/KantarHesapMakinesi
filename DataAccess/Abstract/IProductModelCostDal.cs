using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductModelCostDal : IEntityRepository<ProductModelCost>
    {
        void CustomAdd(ProductModelCost product);
        List<ProductModelCostDto> GetAllProductModelCostDtoById(Expression<Func<ProductModelCostDto, bool>> filter = null);
        ProductModelCostDto GetProductModelCostDtoById(Expression<Func<ProductModelCostDto, bool>> filter = null);
    }
}
