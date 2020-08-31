using System;
using System.Collections.Generic;

namespace CurrencyMonitor.Core.Contracts
{
    public interface ICurrencyExternalService
    {
        IList<ICurrencyData> GetLastRate();

        IList<ICurrencyData> GetHistorical(DateTime date);

        IList<ICurrencyName> GetCurrencyNameList();
    }
}