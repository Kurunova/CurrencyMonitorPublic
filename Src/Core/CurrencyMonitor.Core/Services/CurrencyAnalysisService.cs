using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurrencyMonitor.Core.Contracts;

namespace CurrencyMonitor.Core.Services
{
    public class CurrencyAnalysisService : ICurrencyAnalysisService
    {
        public string Verdict(string mainCurrencyType, IEnumerable<string> currencyTypes, IEnumerable<ICurrencyData> data, IEnumerable<ICurrencyData> history)
        {
            var result = new StringBuilder();
            foreach (var currencyType in currencyTypes)
            {
                var mainCurrency = data.FirstOrDefault(p => p.Type == mainCurrencyType);
                var watchCurrency = data.FirstOrDefault(p => p.Type == currencyType);
                var currentCurrencyRate = mainCurrency.Rate / watchCurrency.Rate;

                var mainCurrencyHistory = history.Where(p => p.Type == mainCurrencyType);
                var watchCurrencyHistory = history.Where(p => p.Type == currencyType);

                var joinHistory = mainCurrencyHistory.Join(
                    watchCurrencyHistory, 
                    main => main.RatingDate, 
                    watch => watch.RatingDate, 
                    (main, watch) => new { watch.RatingDate, MainRate = main.Rate, WatchRate = watch.Rate }).ToList();

                var rates = joinHistory.Select(p => p.MainRate / p.WatchRate).ToList();
                
                var verdict = CurrencyVerdict(watchCurrency.Type, currentCurrencyRate, rates);
                result.AppendLine(verdict);
            }

            return result.ToString();
        }

        private string CurrencyVerdict(string currencyType, double currencyRate, List<double> rates)
        {
            var rateSum = rates.Sum(p => p);
            var average = rateSum / rates.Count;

            var rateMin = rates.Min(p => p);
            var rateMax = rates.Max(p => p);
            var middle = (rateMax + rateMin) / 2;

            string verdict = currencyRate < average ? $"МОЖНО ПОКУПАТЬ {currencyType}\r\n" : "";
            string message = currencyRate < average ? "меньше" : "больше";

            var result = $"{verdict}Курс {currencyType} {currencyRate:F} {message} среднего average={average:F}, middle={middle:F}. \r\n(min={rateMin:F}, max={rateMax:F}, count={rates.Count})";

            return result;
        }
    }
}