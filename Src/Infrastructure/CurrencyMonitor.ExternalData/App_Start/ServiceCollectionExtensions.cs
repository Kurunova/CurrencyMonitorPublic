using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.ExternalData.Configurations;
using CurrencyMonitor.ExternalData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyMonitor.ExternalData
{
	public static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddExternalData(this IServiceCollection services, IConfiguration configuration)
		{
            var exchangeRatesServiceConfiguration = new OpenExchangeRatesServiceConfiguration();
            configuration.Bind("OpenExchangeRates", exchangeRatesServiceConfiguration);
            services.AddSingleton(exchangeRatesServiceConfiguration);
            services.AddScoped<ICurrencyExternalService, OpenExchangeRatesService>();
            return services;
        }
	}
}