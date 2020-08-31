using System;
using System.Collections.Generic;
using CurrencyMonitor.Core.Entities;

namespace CurrencyMonitor.Core.Contracts
{
    public interface ICurrencyDataService
    {
        void AddData(DateTime ratingDate, IEnumerable<ICurrencyData> data);

        IEnumerable<CurrencyHistory> GetData(DateTime start, DateTime end);
    }
}