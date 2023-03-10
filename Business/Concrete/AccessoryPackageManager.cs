using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.AccessoryPackage;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AccessoryPackageManager : IAccessoryPackageService
    {
        IAccessoryPackageDal _accessoryPackageDal;
        public AccessoryPackageManager(IAccessoryPackageDal accessoryPackageDal)
        {
            _accessoryPackageDal = accessoryPackageDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(AccessoryPackageValidator))]
        public IResult Add(AccessoryPackage accessoryPackage)
        {
            if (accessoryPackage != null)
            {
                _accessoryPackageDal.Add(accessoryPackage);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(AccessoryPackage accessoryPackage)
        {
            if (accessoryPackage != null)
            {
                _accessoryPackageDal.Delete(accessoryPackage);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }

        public IDataResult<List<AccessoryPackage>> GetAllAccessoryPackage()
        {
            var result = _accessoryPackageDal.GetAll();
            if (result != null )
            {
                return new SuccessDataResult<List<AccessoryPackage>>(result);
            }
            return new ErrorDataResult<List<AccessoryPackage>>();
        }

        public IDataResult<AccessoryPackage> GetById(int id)
        {
            var result = _accessoryPackageDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<AccessoryPackage>(result);
            }
            return new ErrorDataResult<AccessoryPackage>();
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(AccessoryPackageValidator))]
        public IResult Update(AccessoryPackage accessoryPackage)
        {
            if (accessoryPackage != null)
            {
                _accessoryPackageDal.Update(accessoryPackage);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
