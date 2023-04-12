using Business.Abstract.SP;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract.SP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.SP
{
    [LogAspect(typeof(FileLogger))]
    public class DailyCalculationManager : IDailyCalculationService
    {
        IDailyCalculationDal _dailyCalculationDal;
        public DailyCalculationManager(IDailyCalculationDal dailyCalculationDal)
        {
            _dailyCalculationDal = dailyCalculationDal;
        }
        public IResult CalculateCostSP()
        {
            _dailyCalculationDal.CostDailyCalculation();
            return new SuccessResult();
        }
        public void DailyTCMBSP()
        {
            _dailyCalculationDal.TCMBDailyCalculation();
        }
    }
}
