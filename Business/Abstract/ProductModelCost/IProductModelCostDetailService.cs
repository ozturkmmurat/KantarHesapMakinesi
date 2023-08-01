using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.ProductModelCost
{
    public interface IProductModelCostDetailService
    {
        
        ProductModelCostDetail CostCalculateDetail(ProductModelCostDetail productModelCostDetail);
        IDataResult<ProductModelCostDetail> GetCalculate(ProductModelCostDetail productModelCostDetail);
    }
}
