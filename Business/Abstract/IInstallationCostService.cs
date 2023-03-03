using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IInstallationCostService
    {
        IDataResult<List<InstallationCost>> GetAllInstallationCost();
        IDataResult<List<InstallationCostDto>> GetAllInstallationCostDto();
        IDataResult<InstallationCost> GetById(int id);
        IDataResult<InstallationCostDto> GetInstallationCostByLocationId(int locationId);
        IResult Add(InstallationCost installationCost);
        IResult Update(InstallationCost installationCost);
        IResult Delete(InstallationCost installationCost);
    }
}
