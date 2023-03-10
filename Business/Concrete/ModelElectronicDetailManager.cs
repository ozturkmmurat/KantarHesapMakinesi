using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.Model;
using Core.Aspects.Autofac.Validation;
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
    public class ModelElectronicDetailManager : IModelElectronicDetailService
    {
        IModelElectronicDetailDal _modelElectronicDetailDal;
        public ModelElectronicDetailManager(IModelElectronicDetailDal modelElectronicDetailDal)
        {
            _modelElectronicDetailDal = modelElectronicDetailDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ModelElectronicDetailValidator))]
        public IResult Add(ModelElectronicDetail modelElectronicDetail)
        {
            if (modelElectronicDetail != null)
            {
                _modelElectronicDetailDal.Add(modelElectronicDetail);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(ModelElectronicDetail modelElectronicDetail)
        {
            if (modelElectronicDetail != null)
            {
                _modelElectronicDetailDal.Delete(modelElectronicDetail);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }

        public IDataResult<List<ModelElectronicDetail>> GetAllModelElectronicDetail()
        {
            var result = _modelElectronicDetailDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<ModelElectronicDetail>>(result);
            }
            return new ErrorDataResult<List<ModelElectronicDetail>>();
        }

        public IDataResult<List<ModelElectronicDetailDto>> GetAllModelElectronicDetailDtoByModelId(int modelId)
        {
            var result = _modelElectronicDetailDal.GetAllModelElectronicDetailByModelId(modelId);
            if (result != null)
            {
                return new SuccessDataResult<List<ModelElectronicDetailDto>>(result);
            }
            return new ErrorDataResult<List<ModelElectronicDetailDto>>();
        }

        public IDataResult<ModelElectronicDetail> GetById(int id)
        {
            var result = _modelElectronicDetailDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<ModelElectronicDetail>(result);
            }
            return new ErrorDataResult<ModelElectronicDetail>();
        }

        public IDataResult<ModelElectronicDetailDto> GetModelAccessoryDetailDtoById(int modelId)
        {
            var result = _modelElectronicDetailDal.GetModelElectronicDetailDtoById(x => x.ModelId == modelId);
            if (result != null)
            {
                return new SuccessDataResult<ModelElectronicDetailDto>(result);
            }
            return new ErrorDataResult<ModelElectronicDetailDto>();
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ModelElectronicDetailValidator))]
        public IResult Update(ModelElectronicDetail modelElectronicDetail)
        {
            if (modelElectronicDetail != null)
            {
                _modelElectronicDetailDal.Update(modelElectronicDetail);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
