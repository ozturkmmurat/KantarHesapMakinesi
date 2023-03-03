using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IModelAccessoryDetailDal : IEntityRepository<ModelAccessoryDetail>
    {
        List<ModelAccessoryDetailDto> GetAllModelAccessoryDetailByModelId(int modelId);
        List<ModelAccessoryDetailDto> GetAllModelAccessoryDetailDto(Expression<Func<ModelAccessoryDetailDto, bool>> filter = null);
        ModelAccessoryDetailDto GetModelAccessoryDetailDtoById(Expression<Func<ModelAccessoryDetailDto, bool>> filter);
    }
}
