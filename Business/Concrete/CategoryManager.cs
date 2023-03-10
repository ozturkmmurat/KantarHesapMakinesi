using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IResult Add(Category category)
        {
            if (category != null)
            {
                _categoryDal.Add(category);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }

        public IResult Delete(Category category)
        {
            if (category != null)
            {
                _categoryDal.Delete(category);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }

        public IDataResult<List<Category>> GetAllCategory()
        {
            var result = _categoryDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Category>>(result);
            }
            return new ErrorDataResult<List<Category>>();
        }

        public IDataResult<Category> GetById(int id)
        {
            var result = _categoryDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Category>(result);
            }
            return new ErrorDataResult<Category>();
        }

        public IResult Update(Category category)
        {
            if (category != null)
            {
                _categoryDal.Update(category);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
