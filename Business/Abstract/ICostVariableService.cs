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
        IResult Add(CostVariable costVariable, decimal xValue = 0, decimal yValue = 0);
        IResult Update(CostVariable costVariable, decimal xValue = 0, decimal yValue = 0);
        IResult Delete(CostVariable costVariable);
        CostVariable CostVariableCalculate(CostVariable costVariable, decimal xValue, decimal yValue);
    }
}
