using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICostVariableService
    {
        IDataResult<List<CostVariable>> GetAllCostVariable();
        IDataResult<CostVariable> GetById(int id);
        IResult Add(CostVariable costVariable);
        IResult Update(CostVariable costVariable);
        IResult Delete(CostVariable costVariable);
    }
}
