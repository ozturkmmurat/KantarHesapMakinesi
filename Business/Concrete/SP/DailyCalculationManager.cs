using Business.Abstract.SP;
using DataAccess.Abstract.SP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.SP
{
    public class DailyCalculationManager : IDailyCalculationService
    {
        IDailyCalculationDal _dailyCalculationDal;
        public DailyCalculationManager(IDailyCalculationDal dailyCalculationDal)
        {
            _dailyCalculationDal = dailyCalculationDal;
        }
        public void CalculateCostSP()
        {
            _dailyCalculationDal.CostDailyCalculation();
        }
        public void DailyTCMBSP()
        {
            _dailyCalculationDal.TCMBDailyCalculation();
        }
    }
}
