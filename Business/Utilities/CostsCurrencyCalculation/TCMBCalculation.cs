using Core.Utilities.ExchangeRate.CurrencyGet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.CostsCurrencyCalculation
{
    public static  class TCMBCalculation
    {
        public static decimal EuroCalculation(decimal CurrencyAmount)
        {
            var EUR = CurrencyGet.GetEUR();
            return CurrencyAmount * EUR;
       }

        public static decimal USDCalculation(decimal CurrencyAmount)
        {
            var USD = CurrencyGet.GetUSD();
            return CurrencyAmount * USD;
        }

        public static decimal TLEuroCalculation(decimal CurrencyAmount)
        {
            var TL = CurrencyGet.GetEUR();
            return CurrencyAmount / TL;
        }
    }
}
