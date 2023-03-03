using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
    public class ModelAccessoryDetailManager : IModelAccessoryDetailService
    {
        IModelAccessoryDetailDal _modelAccessoryDetailDal;
        public ModelAccessoryDetailManager(IModelAccessoryDetailDal modelAccessoryDetailDal)
        {
            _modelAccessoryDetailDal = modelAccessoryDetailDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(ModelAccessoryDetail modelAccessoryDetail)
        {
            if (modelAccessoryDetail != null)
            {
                _modelAccessoryDetailDal.Add(modelAccessoryDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(ModelAccessoryDetail modelAccessoryDetail)
        {
            if (modelAccessoryDetail != null)
            {
                _modelAccessoryDetailDal.Delete(modelAccessoryDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<ModelAccessoryDetail>> GetAllModelAccessoryDetail()
        {
            var result = _modelAccessoryDetailDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<ModelAccessoryDetail>>(result);
            }
            return new ErrorDataResult<List<ModelAccessoryDetail>>();
        }

        public IDataResult<List<ModelAccessoryDetailDto>> GetAllModelAccessoryDetailDto()
        {
            var result = _modelAccessoryDetailDal.GetAllModelAccessoryDetailDto();
            if (result != null)
            {
                return new SuccessDataResult<List<ModelAccessoryDetailDto>>();
            }
            return new ErrorDataResult<List<ModelAccessoryDetailDto>>();
        }

        public IDataResult<List<ModelAccessoryDetailDto>> GetAllModelAccessoryDetailDtoById(int modelId)
        {
            var result = _modelAccessoryDetailDal.GetAllModelAccessoryDetailByModelId(modelId);
            if (result != null)
            {
                return new SuccessDataResult<List<ModelAccessoryDetailDto>>(result);
            }
            return new ErrorDataResult<List<ModelAccessoryDetailDto>>();
        }

        public IDataResult<ModelAccessoryDetail> GetById(int id)
        {
            var result = _modelAccessoryDetailDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<ModelAccessoryDetail>(result);
            }
            return new ErrorDataResult<ModelAccessoryDetail>();
        }

        public IDataResult<ModelAccessoryDetailDto> GetModelAccesoryDetailDtoByModelId(int modelId)
        {
            var result = _modelAccessoryDetailDal.GetModelAccessoryDetailDtoById(x => x.ModelId == modelId);
            if (result != null)
            {
                return new SuccessDataResult<ModelAccessoryDetailDto>(result);
            }
            return new ErrorDataResult<ModelAccessoryDetailDto>();
        }
        [SecuredOperation("admin")]
        public IResult Update(ModelAccessoryDetail modelAccessoryDetail)
        {
            if (modelAccessoryDetail != null)
            {
                _modelAccessoryDetailDal.Update(modelAccessoryDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
