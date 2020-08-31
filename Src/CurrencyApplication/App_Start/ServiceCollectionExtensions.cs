using CurrencyMonitor.Core;
using CurrencyMonitor.EFDataAccess;
using CurrencyMonitor.ExternalData;
using CurrencyMonitor.NotifyService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CurrencyMonitor.Application
{
	public static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddNotifier(configuration);
            services.AddExternalData(configuration);
            services.AddDataAccess(configuration);
            services.AddCore(configuration);

            services.AddLogging(configure => configure.AddSerilog())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            return services;
		}
	}
}