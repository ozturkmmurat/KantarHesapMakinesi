using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IVariantService
    {
        IDataResult<List<Variant>> GetAllVariant();
        IDataResult<Variant> GetById(int id);
        IResult Add(Variant variant);
        IResult Update(Variant variant);
        IResult Delete(Variant variant);
    }
}
