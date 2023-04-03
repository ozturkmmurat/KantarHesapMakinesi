using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.Utilities.CostsCurrencyCalculation;
using Business.ValidationRules.FluentValidation.Accessory;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AccessoryManager : IAccessoryService
    {
        IAccessoryDal _accessoryDal;
        public AccessoryManager(IAccessoryDal accessoryDal)
        {
            _accessoryDal = accessoryDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(AccessoryValidator))]
        public IResult Add(Accessory accessory)
        {
            if (accessory != null)
            {
                accessory.AccessoryTlPrice = TCMBCalculation.EuroCalculation(accessory.AccessoryEuroPrice);
                _accessoryDal.Add(accessory);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
            
        }
        [SecuredOperation("admin")]
        public IResult Delete(Accessory accessory)
        {
            if (accessory != null)
            {
                _accessoryDal.Delete(accessory);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<Accessory>> GetAllAccessory()
        {
            var result = _accessoryDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Accessory>>(result);
            }
            return new ErrorDataResult<List<Accessory>>();
        }
        public IDataResult<Accessory> GetById(int id)
        {
            var result = _accessoryDal.Get(x => x.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<Accessory>(result);
            }
            return new ErrorDataResult<Accessory>();
        }
        [SecuredOperation("admin")]
        public IDataResult<Accessory> GetByName(string name)
        {
            var result = _accessoryDal.Get(x => x.AccessoryName == name);
            if (result != null)
            {
                return new SuccessDataResult<Accessory>(result);
            }
            return new ErrorDataResult<Accessory>();
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(AccessoryValidator))]
        public IResult Update(Accessory accessory)
        {
            if(accessory != null)
            {
                accessory.AccessoryTlPrice = TCMBCalculation.EuroCalculation(accessory.AccessoryEuroPrice);
                _accessoryDal.Update(accessory);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
