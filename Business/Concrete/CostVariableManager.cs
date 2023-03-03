using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CostVariableManager : ICostVariableService
    {
        ICostVariableDal _costVariableDal;
        public CostVariableManager(ICostVariableDal costVariableDal)
        {
            _costVariableDal = costVariableDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(CostVariable costVariable, decimal xValue = 0, decimal yValue = 0)
        {
            var result = CostVariableCalculate(costVariable,xValue,yValue);
            if (result != null)
            {
                _costVariableDal.Add(result);
                return new SuccessResult();
            }
            return new ErrorResult();
        }


        public CostVariable CostVariableCalculate(CostVariable costVariable, decimal xValue, decimal yValue)
        {
            if (costVariable != null && xValue != 0 && yValue != 0)
            {
                costVariable.ShateIron = xValue / yValue;
                return costVariable;
            }
            return costVariable;
        }
        [SecuredOperation("admin")]
        public IResult Delete(CostVariable costVariable)
        {
            if (costVariable != null && costVariable.Id != 7)
            {
                _costVariableDal.Delete(costVariable);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        public IDataResult<List<CostVariable>> GetAllCostVariable()
        {
            var result = _costVariableDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<CostVariable>>(result);
            }
            return new ErrorDataResult<List<CostVariable>>();
        }

        public IDataResult<CostVariable> GetById(int id)
        {
            var result = _costVariableDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<CostVariable>(result);
            }
            return new ErrorDataResult<CostVariable>();
        }
        [SecuredOperation("admin")]
        public IResult Update(CostVariable costVariable, decimal xValue = 0, decimal yValue = 0)
        {
            var result = CostVariableCalculate(costVariable, xValue, yValue);
            if (result != null && costVariable.Id != 7)
            {
                _costVariableDal.Update(result);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
