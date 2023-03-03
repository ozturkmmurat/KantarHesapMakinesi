using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IModelElectronicDetailDal : IEntityRepository<ModelElectronicDetail>
    {
        List<ModelElectronicDetailDto> GetAllModelElectronicDetailByModelId(int modelId);
        ModelElectronicDetailDto GetModelElectronicDetailDtoById(Expression<Func<ModelElectronicDetailDto, bool>> filter);
    }
}
