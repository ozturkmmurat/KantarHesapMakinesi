using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.ProductModelCostDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductModelCostDetailService
    {
        IDataResult<List<Entities.Concrete.ProductModelCostDetail>> GetAllInstallationCostDetail();
        IDataResult<Entities.Concrete.ProductModelCostDetail> GetById(int id);
        IDataResult<ProductModelCostDetailDto> GetProductModelCostDetailLocationModelId(int locationId, int modelId);
        IResult Add(Entities.Concrete.ProductModelCostDetail productModelCostDetail);
        IResult Update(Entities.Concrete.ProductModelCostDetail productModelCostDetail);
        IResult Delete(Entities.Concrete.ProductModelCostDetail productModelCostDetail);
    }
}
