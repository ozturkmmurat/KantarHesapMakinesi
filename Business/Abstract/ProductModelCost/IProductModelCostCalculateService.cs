using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.ProductModelCost
{
    public interface IProductModelCostCalculateService
    {
        IDataResult <List<Entities.Concrete.ProductModelCost>> AddProductModelCost(ProductModelCostDto productModelCostDto);
        IDataResult<List<ProductModelCostDto>> UpdateProductModelCost(ProductModelCostDto productModelCostDto);
        IDataResult<List<ProductModelCostDto>> CostCalculate(ProductModelCostDto productModelCostDto);
        IDataResult<List<ProductModelCostDto>> CostCalculateList(List<ProductModelCostDto> productModelCostDtos);
        IDataResult<Entities.Concrete.ProductModelCost> MappingProductModelCost(ProductModelCostDto productModelCostDto);
        IDataResult<List<Entities.Concrete.ProductModelCost>> MappingProductModelCostList(List<ProductModelCostDto> productModelCostDtos);
    }
}
