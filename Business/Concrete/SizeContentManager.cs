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
    public class SizeContentManager : ISizeContentService
    {
        ISizeContentDal _sizeContent;
        public SizeContentManager(ISizeContentDal sizeContent)
        {
            _sizeContent = sizeContent;
        }
        [SecuredOperation("admin")]
        public IResult Add(SizeContent modelHeightWeight)
        {
            if (modelHeightWeight  != null)
            {
                _sizeContent.Add(modelHeightWeight);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(SizeContent modelHeightWeight)
        {
            if (modelHeightWeight != null)
            {
                _sizeContent.Delete(modelHeightWeight);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        public IDataResult<List<SizeContentDto>> GetAllSizeCtDtoBySizeId(int sizeId)
        {
            var result = _sizeContent.GetAllSizeContentDto(x => x.SizeId == sizeId);
            if (result != null)
            {
                return new SuccessDataResult<List<SizeContentDto>>(result, Messages.GetByAll);
            }
            return new ErrorDataResult<List<SizeContentDto>>(result, Messages.GetByAllDefault);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<SizeContent>> GetAllSizeContent()
        {
            var result = _sizeContent.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<SizeContent>>(result, Messages.GetByAll);
            }
            return new ErrorDataResult<List<SizeContent>>(Messages.GetByAllDefault);
        }
        public IDataResult<SizeContent> GetById(int id)
        {
            var result = _sizeContent.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<SizeContent>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<SizeContent>(Messages.GetByAllDefault);
        }
        [SecuredOperation("admin")]
        public IResult Update(SizeContent modelHeightWeight)
        {
            if (modelHeightWeight != null)
            {
                _sizeContent.Update(modelHeightWeight);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
