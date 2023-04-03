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
        IDataResult<List<Entities.Concrete.ProductModelCost>> GetAllProductModelCost();
        IDataResult<Entities.Concrete.ProductModelCost> GetById(int id);
        IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelId(int modelId);
        IDataResult<Entities.Concrete.ProductModelCost> MappingProductModelCost(ProductModelCostDto productModelCostDto);
        IResult Add(Entities.Concrete.ProductModelCost productModelCost);
        IResult AddProductModelCost(ProductModelCostDto productModelCostDto);
        IResult Update(Entities.Concrete.ProductModelCost productModelCost);
        IResult UpdateProductModelCost(ProductModelCostDto productModelCostDto);
        IResult Delete(Entities.Concrete.ProductModelCost productModelCost);
        ProductModelCostDto CostCalculate(ProductModelCostDto productModelCostDto);
    }
}
