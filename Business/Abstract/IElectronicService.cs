using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IElectronicService
    {
        IDataResult<List<Electronic>> GetAllElectronic();
        IDataResult<Electronic> GetById(int id);
        IResult Add(Electronic electronic);
        IResult Update(Electronic electronic);
        IResult Delete(Electronic electronic);
    }
}
