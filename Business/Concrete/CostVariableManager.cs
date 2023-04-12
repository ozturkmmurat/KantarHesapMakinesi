using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.CostVariable;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class CostVariableManager : ICostVariableService
    {
        ICostVariableDal _costVariableDal;
        public CostVariableManager(ICostVariableDal costVariableDal)
        {
            _costVariableDal = costVariableDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CostVariableValidator))]
        public IResult Add(CostVariable costVariable)
        {
            if (costVariable != null)
            {
                _costVariableDal.Add(costVariable);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(CostVariable costVariable)
        {
            if (costVariable != null && costVariable.Id != 7)
            {
                _costVariableDal.Delete(costVariable);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<CostVariable>> GetAllCostVariable()
        {
            var result = _costVariableDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<CostVariable>>(result);
            }
            return new ErrorDataResult<List<CostVariable>>();
        }
        [SecuredOperation("admin")]
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
        [ValidationAspect(typeof(CostVariableValidator))]
        public IResult Update(CostVariable costVariable)
        {
            if (costVariable != null)
            {
                _costVariableDal.Update(costVariable);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
