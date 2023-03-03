using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccessoryPackageService
    {
        IDataResult<List<AccessoryPackage>> GetAllAccessoryPackage();
        IDataResult<AccessoryPackage> GetById(int id);
        IResult Add(AccessoryPackage accessoryPackage);
        IResult Update(AccessoryPackage accessoryPackage);
        IResult Delete(AccessoryPackage accessoryPackage);
    }
}
