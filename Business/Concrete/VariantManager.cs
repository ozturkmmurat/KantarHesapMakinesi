using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class VariantManager : IVariantService
    {
        IVariantDal _variantDal;
        public VariantManager(IVariantDal variantDal)
        {
            _variantDal = variantDal;
        }
        public IResult Add(Variant variant)
        {
            if (variant != null)
            {
                _variantDal.Add(variant);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(Variant variant)
        {
            if (variant != null)
            {
                _variantDal.Delete(variant);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<Variant>> GetAllVariant()
        {
            var result = _variantDal.GetAll();
            if (result.Count != null )
            {
                return new SuccessDataResult<List<Variant>>(result);
            }
            return new ErrorDataResult<List<Variant>>();
        }

        public IDataResult<Variant> GetById(int id)
        {
            var result = _variantDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Variant>();
            }
            return new ErrorDataResult<Variant>();
        }

        public IResult Update(Variant variant)
        {
            if (variant != null)
            {
                _variantDal.Update(variant);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
