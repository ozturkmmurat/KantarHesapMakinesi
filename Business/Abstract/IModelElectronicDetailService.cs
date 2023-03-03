using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IModelElectronicDetailService
    {
        IDataResult<List<ModelElectronicDetail>> GetAllModelElectronicDetail();
        IDataResult<List<ModelElectronicDetailDto>> GetAllModelElectronicDetailDtoByModelId(int modelId);
        IDataResult<ModelElectronicDetail> GetById(int id);
        IDataResult<ModelElectronicDetailDto> GetModelAccessoryDetailDtoById(int modelId);
        IResult Add(ModelElectronicDetail modelElectronicDetail);
        IResult Update(ModelElectronicDetail modelElectronicDetail);
        IResult Delete(ModelElectronicDetail modelElectronicDetail);
    }
}
