using Core.Utilities.ExchangeRate.CurrencyGet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.CostsCurrencyCalculation
{
    public static  class TCMBCalculation
    {
        public static decimal CurrencyCalculation(decimal currencyAmount, decimal currency)
        {
            return currencyAmount * currency;
        }

        public static decimal DivideCurrencyCalculation(decimal currencyAmount, decimal currency)
        {
            return currencyAmount / currency;
        }

        public static decimal EuroBasedCurrencyCalculate(decimal currencyAmount, decimal currency, decimal calculateCurrency)
        {
            var result = calculateCurrency / currency;
            var x = Math.Round(result, 4);
            return currencyAmount * x;
        }
    }
}
