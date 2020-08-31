using CurrencyMonitor.Core.Contracts;

namespace CurrencyMonitor.ExternalData.Models
{
    public class CurrencyData : ICurrencyData
    {
        public string Type { get; set; }

        public double Rate { get; set; }
    }
}