using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ElectronicManager : IElectronicService
    {
        IElectronicDal _electronicDal;
        public ElectronicManager(IElectronicDal electronicDal)
        {
            _electronicDal = electronicDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(Electronic electronic)
        {
            if (electronic != null)
            {
                electronic.ElectronicTlPrice = TCMBCalculation.EuroCalculation(electronic.ElectronicEuroPrice);
                _electronicDal.Add(electronic);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(Electronic electronic)
        {
            if (electronic != null)
            {
                _electronicDal.Delete(electronic);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<Electronic>> GetAllElectronic()
        {
            var result = _electronicDal.GetAll();
            if (result !=null)
            {
                return new SuccessDataResult<List<Electronic>>(result);
            }
            return new ErrorDataResult<List<Electronic>>();
        }

        public IDataResult<Electronic> GetById(int id)
        {
            var result = _electronicDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Electronic>(result);
            }
            return new ErrorDataResult<Electronic>();
        }
        [SecuredOperation("admin")]
        public IResult Update(Electronic electronic)
        {
            if (electronic != null)
            {
                electronic.ElectronicTlPrice = TCMBCalculation.EuroCalculation(electronic.ElectronicEuroPrice);
                _electronicDal.Update(electronic);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
