using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccessoryService
    {
        IDataResult<List<Accessory>> GetAllAccessory();
        IDataResult<Accessory> GetById(int id);
        IDataResult<Accessory> GetByName(string name);
        IResult Add(Accessory accessory);
        IResult Update(Accessory accessory);
        IResult Delete(Accessory accessory);
    }
}
