using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.SP
{
    public interface IDailyCalculationDal
    {
        void CostDailyCalculation();
        void TCMBDailyCalculation();
    }
}
