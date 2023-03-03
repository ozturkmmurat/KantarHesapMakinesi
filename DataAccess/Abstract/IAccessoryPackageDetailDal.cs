using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAccessoryPackageDetailDal : IEntityRepository<AccessoryPackageDetail>
    {
        List<AccessoryPackageDetailDto> GetAllModelAccessoryDetailDto(Expression<Func<AccessoryPackageDetailDto, bool>> filter = null);
        AccessoryPackageDetailDto GetAccessoryPackageDetailById(Expression<Func<AccessoryPackageDetailDto, bool>> filter = null);
    }
}
