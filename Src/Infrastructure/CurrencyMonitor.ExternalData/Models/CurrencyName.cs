using CurrencyMonitor.Core.Contracts;

namespace CurrencyMonitor.ExternalData.Models
{
    public class CurrencyName : ICurrencyName
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}