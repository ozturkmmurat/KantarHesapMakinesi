using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductModelCostService
    {
        IDataResult<List<ProductModelCost>> GetAllProductModelCost();
        IDataResult<ProductModelCost> GetById(int id);
        IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelId(int modelId);
        IDataResult<ProductModelCost> MappingProductModelCost(ProductModelCostDto productModelCostDto);
        IDataResult<Entities.Concrete.ProductModelCostDetail> MappingProductModelCostDetail(ProductModelCostDto productModelCostDto);
        IResult Add(ProductModelCost productModelCost);
        IResult AddProductModelCost(ProductModelCostDto productModelCostDto);
        IResult Update(ProductModelCost productModelCost);
        IResult UpdateProductModelCost(ProductModelCostDto productModelCostDto);
        IResult Delete(ProductModelCost productModelCost);
        ProductModelCostDto CostCalculate(ProductModelCostDto productModelCostDto);
    }
}
