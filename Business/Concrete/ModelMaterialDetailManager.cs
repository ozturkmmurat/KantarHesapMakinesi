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
    public class ModelMaterialDetailManager : IModelMaterialDetailService
    {
        IModelMaterialDetailDal _modelDetailDal;
        public ModelMaterialDetailManager(IModelMaterialDetailDal modelDetailDal)
        {
            _modelDetailDal = modelDetailDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(ModelMaterialDetail modelDetail)
        {
            if (modelDetail != null)
            {
                _modelDetailDal.Add(modelDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(ModelMaterialDetail modelDetail)
        {
            if (modelDetail != null)
            {
                _modelDetailDal.Delete(modelDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<ModelMaterialDetail>> GetAllModelDetail()
        {
            var result = _modelDetailDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<ModelMaterialDetail>>(result);
            }
            return new ErrorDataResult<List<ModelMaterialDetail>>();
        }

        public IDataResult<List<ModelMaterialDetailDto>> GetAllModelDetailDtoById(int modelId, int productId)
        {
            var result = _modelDetailDal.GetModelDetailsByModelId(modelId, productId);
            if (result != null)
            {
                return new SuccessDataResult<List<ModelMaterialDetailDto>>(result);
            }
            return new ErrorDataResult<List<ModelMaterialDetailDto>>();
        }

        public IDataResult<ModelMaterialDetail> GetById(int id)
        {
            var result = _modelDetailDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<ModelMaterialDetail>(result);
            }
            return new ErrorDataResult<ModelMaterialDetail>();
        }
        [SecuredOperation("admin")]
        public IResult Update(ModelMaterialDetail modelDetail)
        {
            if (modelDetail != null)
            {
                _modelDetailDal.Update(modelDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
