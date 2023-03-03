using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class InstallationCostManager : IInstallationCostService
    {
        IInstallationCostDal _installationCostDal;
        public InstallationCostManager(IInstallationCostDal installationCostDal)
        {
            _installationCostDal = installationCostDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(InstallationCost installationCost)
        {
            if (installationCost != null)
            {
                installationCost.InstallationTlPrice = TCMBCalculation.EuroCalculation(installationCost.InstallationEuroPrice);
                _installationCostDal.Add(installationCost);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]

        public IResult Delete(InstallationCost installationCost)
        {
            if (installationCost != null)
            {
                _installationCostDal.Delete(installationCost);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<InstallationCost>> GetAllInstallationCost()
        {
            var result = _installationCostDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<InstallationCost>>(result);
            }
            return new ErrorDataResult<List<InstallationCost>>();
        }

        public IDataResult<List<InstallationCostDto>> GetAllInstallationCostDto()
        {
            var result = _installationCostDal.GetAllInstallationCostDto();
            if (result != null)
            {
                return new SuccessDataResult<List<InstallationCostDto>>(result);
            }
            return new ErrorDataResult<List<InstallationCostDto>>();
        }

        public IDataResult<InstallationCost> GetById(int id)
        {
            var result = _installationCostDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<InstallationCost>(result);
            }
            return new ErrorDataResult<InstallationCost>();
        }

        public IDataResult<InstallationCostDto> GetInstallationCostByLocationId(int locationId)
        {
            var result = _installationCostDal.GetByIdProductModelCostDetailDto(x=> x.LocationId == locationId);
            if (result != null)
            {
                return new SuccessDataResult<InstallationCostDto>(result);
            }
            return new ErrorDataResult<InstallationCostDto>();
        }
        [SecuredOperation("admin")]
        public IResult Update(InstallationCost installationCost)
        {
            if (installationCost != null)
            {
                installationCost.InstallationTlPrice = TCMBCalculation.EuroCalculation(installationCost.InstallationEuroPrice);
                _installationCostDal.Update(installationCost);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
