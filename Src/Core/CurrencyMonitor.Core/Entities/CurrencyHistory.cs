using System;
using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.Core.Contracts.DataAccess;

namespace CurrencyMonitor.Core.Entities
{
    public class CurrencyHistory : IEntity, ICurrencyData
    {
        public int Id { get; set; }
        
        public string Type { get; set; }

        public double Rate { get; set; }

        public DateTime RatingDate { get; set; }

        public DateTime DateCreated { get; set; }
    }
}