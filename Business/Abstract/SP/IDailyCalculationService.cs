using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.SP
{
    public interface IDailyCalculationService
    {
        void CalculateCostSP();
        void DailyTCMBSP();
    }
}
