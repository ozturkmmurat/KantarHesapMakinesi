using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LocationManager : ILocationService
    {
        ILocationDal _locationDal;
        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(Location location)
        {
            if (location != null)
            {
                _locationDal.Add(location);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(Location location)
        {
            if (location != null)
            {
                _locationDal.Delete(location);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IDataResult<List<Location>> GetAllLocation()
        {
            var result = _locationDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Location>>(result);
            }
            return new ErrorDataResult<List<Location>>();
        }
        [SecuredOperation("admin")]
        public IDataResult<Location> GetById(int id)
        {
            throw new NotImplementedException();
        }
        [SecuredOperation("admin")]
        public IResult Update(Location location)
        {
            if (location != null)
            {
                _locationDal.Update(location);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
