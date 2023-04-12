using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.SP
{
    public interface IDailyCalculationService
    {
        IResult CalculateCostSP();
        void DailyTCMBSP();
    }
}
