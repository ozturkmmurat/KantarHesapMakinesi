using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Utilities.CostsCurrencyCalculation;
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
        public IResult Add(Accessory accessory)
        {
            if (accessory != null)
            {
                accessory.AccessoryTlPrice = TCMBCalculation.EuroCalculation(accessory.AccessoryEuroPrice);
                _accessoryDal.Add(accessory);
                return new SuccessResult();
            }
            return new ErrorResult();
            
        }
        [SecuredOperation("admin")]
        public IResult Delete(Accessory accessory)
        {
            if (accessory != null)
            {
                _accessoryDal.Delete(accessory);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

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
        public IResult Update(Accessory accessory)
        {
            if(accessory != null)
            {
                accessory.AccessoryTlPrice = TCMBCalculation.EuroCalculation(accessory.AccessoryEuroPrice);
                _accessoryDal.Update(accessory);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
