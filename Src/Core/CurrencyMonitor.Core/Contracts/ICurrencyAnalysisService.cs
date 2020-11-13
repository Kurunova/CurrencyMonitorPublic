using System.Collections.Generic;

namespace CurrencyMonitor.Core.Contracts
{
    public interface ICurrencyAnalysisService
    {
        string Verdict(string mainCurrencyType, IEnumerable<string> currencyType, IEnumerable<ICurrencyData> data, IEnumerable<ICurrencyData> history);
    }
}