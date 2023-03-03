using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IModelMaterialDetailService
    {
        IDataResult<List<ModelMaterialDetail>> GetAllModelDetail();
        IDataResult<List<ModelMaterialDetailDto>> GetAllModelDetailDtoById(int modelId, int productId);
        IDataResult<ModelMaterialDetail> GetById(int id);
        IResult Add(ModelMaterialDetail modelDetail);
        IResult Update(ModelMaterialDetail modelDetail);
        IResult Delete(ModelMaterialDetail modelDetail);
    }
}
