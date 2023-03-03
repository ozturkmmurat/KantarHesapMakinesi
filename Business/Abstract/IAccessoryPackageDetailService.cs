using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccessoryPackageDetailService
    {
        IDataResult<List<AccessoryPackageDetail>> GetAllAccessoryPackage();
        IDataResult<List<AccessoryPackageDetailDto>> GetAllAccessoryPackageDto();
        IDataResult<List<AccessoryPackageDetailDto>> GetAllAccessoryPackageDtoById(int id);
        IDataResult<AccessoryPackageDetailDto> GetByAccessoryPackageDetail(int id);
        IDataResult<AccessoryPackageDetail> GetById(int id);
        IResult Add(AccessoryPackageDetail accessoryPackageDetail);
        IResult Update(AccessoryPackageDetail accessoryPackageDetail);
        IResult Delete(AccessoryPackageDetail accessoryPackageDetail);
    }
}
