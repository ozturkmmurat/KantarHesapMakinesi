
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
        IDataResult<List<ProductModelCostDto>> GetAllDto();
        IDataResult<Entities.Concrete.ProductModelCost> GetById(int id);
        IDataResult<Entities.Concrete.ProductModelCost> GetByModelIdCurrency(int modelId, string currencyName);
        IDataResult<ProductModelCostDto> GetProductModelCostDtoByModelIdCurrency(int modelId, string currencyName);
        IResult Add(Entities.Concrete.ProductModelCost productModelCost);
        IResult AddProductModelCost(ProductModelCostDto productModelCostDto);
        IResult Update(Entities.Concrete.ProductModelCost productModelCost);
        IResult UpdateProductModelCost(ProductModelCostDto productModelCostDto);
        IResult UpdateRange(List<ProductModelCostDto> productModelCostDtos);
        IResult Delete(Entities.Concrete.ProductModelCost productModelCost);
    }
}
