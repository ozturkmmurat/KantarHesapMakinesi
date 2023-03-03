using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IModelAccessoryDetailService
    {
        IDataResult<List<ModelAccessoryDetail>> GetAllModelAccessoryDetail();
        IDataResult<List<ModelAccessoryDetailDto>> GetAllModelAccessoryDetailDtoById(int modelId);
        IDataResult<List<ModelAccessoryDetailDto>> GetAllModelAccessoryDetailDto();
        IDataResult<ModelAccessoryDetail> GetById(int id);
        IDataResult<ModelAccessoryDetailDto> GetModelAccesoryDetailDtoByModelId(int modelId);
        IResult Add(ModelAccessoryDetail modelAccessoryDetail);
        IResult Update(ModelAccessoryDetail modelAccessoryDetail);
        IResult Delete(ModelAccessoryDetail modelAccessoryDetail);
    }
}
