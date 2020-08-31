using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.Core.Contracts.DataAccess;
using CurrencyMonitor.Core.Entities;

namespace CurrencyMonitor.Core.Services
{
    public class CurrencyDataService : ICurrencyDataService
    {
        private readonly IRepository<CurrencyHistory> _repository;

        public CurrencyDataService(IRepository<CurrencyHistory> repository)
        {
            _repository = repository;
        }

        public void AddData(DateTime ratingDate, IEnumerable<ICurrencyData> data)
        {
            var currencyHistories = data.Select(p => new CurrencyHistory {DateCreated = DateTime.Now, Rate = p.Rate, Type = p.Type, RatingDate = ratingDate });
            _repository.Add(currencyHistories);
        }

        public IEnumerable<CurrencyHistory> GetData(DateTime start, DateTime end)
        {
            var result = _repository.Find(p => start <= p.RatingDate && p.RatingDate <= end);
            return result;
        }
    }
}