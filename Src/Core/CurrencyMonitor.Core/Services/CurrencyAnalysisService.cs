using System.Collections.Generic;
using System.Linq;
using CurrencyMonitor.Core.Contracts;

namespace CurrencyMonitor.Core.Services
{
    public class CurrencyAnalysisService : ICurrencyAnalysisService
    {
        public string Verdict(string currencyType, IEnumerable<ICurrencyData> data, IEnumerable<ICurrencyData> history)
        {
            var currentCurrency = data.FirstOrDefault(p => p.Type == currencyType);
            var currentCurrencyHistory = history.Where(p => p.Type == currencyType);
            var rates = currentCurrencyHistory.Select(p => p.Rate).ToList();
            var rateSum = rates.Sum(p => p);
            var average = rateSum / rates.Count;

            var rat = history.OrderBy(p => p.Rate);

            var rateMin = rates.Min(p => p);
            var rateMax = rates.Max(p => p);
            var middle = (rateMax + rateMin) / 2;

            string verdict = currentCurrency.Rate < average ? "МОЖНО ПОКУПАТЬ\r\n" : "";
            string message = currentCurrency.Rate < average ? "меньше" : "больше";

            var result = $"{verdict}Курс {currentCurrency.Rate:C} {message} среднего (min={rateMin:C}, max={rateMax:C}, count={rates.Count}).\r\naverage={average:C}\r\nmiddle={middle:C}";

            return result;
        }
    }
}