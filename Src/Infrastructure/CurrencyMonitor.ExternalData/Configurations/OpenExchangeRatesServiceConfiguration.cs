namespace CurrencyMonitor.ExternalData.Configurations
{
    public class OpenExchangeRatesServiceConfiguration
    {
        public string Host { get; set; } = "https://openexchangerates.org";

        public string ApiId { get; set; }
    }
}