using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
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
    [LogAspect(typeof(FileLogger))]
    public class SizeManager : ISizeService
    {
        ISizeDal _sizeDal;
        public SizeManager(ISizeDal sizeDal)
        {
            _sizeDal = sizeDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(Size size)
        {
            if (size != null)
            {
                _sizeDal.Add(size);
                return new SuccessResult(Messages.DataAdded);

            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(Size size)
        {
            if (size != null)
            {
                _sizeDal.Delete(size);
                return new SuccessResult(Messages.DataDeleted);

            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        [SecuredOperation("admin")]
        public IDataResult<Size> GetBySize(string aspect, string weight)
        {
            var result = _sizeDal.Get(x => x.Aspect == aspect && x.Weight == weight);
            if (result != null)
            {
                return new SuccessDataResult<Size>(result ,Messages.GetByAll);
            }
            return new ErrorDataResult<Size>(Messages.GetByAllDefault);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<Size>> GetAllSize()
        {
            var result = _sizeDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Size>>(result,Messages.GetByAll);
            }
            return new ErrorDataResult<List<Size>>(Messages.GetByAllDefault);
        }

        public IDataResult<Size> GetById(int id)
        {
            var result = _sizeDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Size>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<Size>(Messages.GetByAllDefault);
        }
        [SecuredOperation("admin")]
        public IResult Update(Size size)
        {
            if (size != null)
            {
                _sizeDal.Update(size);
                return new SuccessResult(Messages.DataUpdate);

            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
