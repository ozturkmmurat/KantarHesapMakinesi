using Core.Utilities.Result.Abstract;
using Entities.Dtos.ProductModelCostDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.ProductModelCostDetail
{
    public interface IProductModelCostDetailSelectListService
    {
        IDataResult<List<ProductModelCostDetailSelectListDto>> GetAllProductModelCostDetailDtoByProductModelCostId(int productModelCostId);
    }
}
