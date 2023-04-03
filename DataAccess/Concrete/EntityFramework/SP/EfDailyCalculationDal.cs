using DataAccess.Abstract.SP;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.SP
{
    public class EfDailyCalculationDal : IDailyCalculationDal
    {
        public void CostDailyCalculation()
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                context.Database.ExecuteSqlCommand("calculateModelCostSP");
            }
        }

        public void TCMBDailyCalculation()
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                context.Database.ExecuteSqlCommand("costCurrencySP");
            }
        }
    }
}
