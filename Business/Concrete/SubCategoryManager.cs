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
    public class SubCategoryManager : ISubCategoryService
    {
        ISubCategoryDal _subCategoryDal;
        public SubCategoryManager(ISubCategoryDal subCategoryDal)
        {
            _subCategoryDal = subCategoryDal;
        }
        public IResult Add(SubCategory subCategory)
        {
            if (subCategory != null)
            {
                _subCategoryDal.Add(subCategory);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(SubCategory subCategory)
        {
            if (subCategory != null)
            {
                _subCategoryDal.Delete(subCategory);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<SubCategory>> GetAllSubCategory()
        {
            var result = _subCategoryDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<SubCategory>>(result);
            }
            return new ErrorDataResult<List<SubCategory>>();
        }

        public IDataResult<SubCategory> GetById(int id)
        {
            var result = _subCategoryDal.Get(x=> x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<SubCategory>(result);
            }
            return new ErrorDataResult<SubCategory>();
        }

        public IResult Update(SubCategory subCategory)
        {
            if (subCategory != null)
            {
                _subCategoryDal.Update(subCategory);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
