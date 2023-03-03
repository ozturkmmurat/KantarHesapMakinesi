using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMaterialService
    {
        IDataResult<List<Material>> GetAllMaterial();
        IDataResult<Material> GetById(int id);
        IResult Add(Material material);
        IResult Update(Material material);
        IResult Delete(Material material);
        
    }
}
