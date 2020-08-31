using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyMonitor.Core
{
	public static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<ICurrencySubscribeService, CurrencySubscribeService>()
                .AddScoped<ICurrencyDataService, CurrencyDataService>()
                .AddScoped<ICurrencyAnalysisService, CurrencyAnalysisService>();

			return services;
        }
	}
}