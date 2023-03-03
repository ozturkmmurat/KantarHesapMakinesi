using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IInstallationCostDal : IEntityRepository<InstallationCost>
    {
        List<InstallationCostDto> GetAllInstallationCostDto(Expression<Func<InstallationCostDto, bool>> filter = null);
        InstallationCostDto GetByIdProductModelCostDetailDto(Expression<Func<InstallationCostDto, bool>> filter);
    }
}
