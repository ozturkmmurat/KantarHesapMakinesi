using Core.DataAccess;
using Entities.Dtos.ProductModelCostDetail;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract.ProductModelCostDetail
{
    public interface IProductModelCostDetailSelectListDal : IEntityRepository<Entities.Concrete.ProductModelCostDetail>
    {
        List<ProductModelCostDetailSelectListDto> GetAllProductModelCostDetailDtos(Expression<Func<ProductModelCostDetailSelectListDto, bool>> filter = null);
    }
}
