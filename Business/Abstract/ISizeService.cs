using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISizeService
    {
        IDataResult<List<Size>> GetAllSize();
        IDataResult<Size> GetBySize(string aspect, string weight);
        IDataResult<Size> GetById(int id);
        IResult Add(Size heightWeight);
        IResult Update(Size heightWeight);
        IResult Delete(Size heightWeight);
    }
}
