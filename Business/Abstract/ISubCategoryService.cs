using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISubCategoryService
    {
        IDataResult<List<SubCategory>> GetAllSubCategory();
        IDataResult<SubCategory> GetById(int id);
        IResult Add(SubCategory subCategory);
        IResult Update(SubCategory subCategory);
        IResult Delete(SubCategory subCategory);
    }
}
