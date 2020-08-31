using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.NotifyService.Configurations;
using CurrencyMonitor.NotifyService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyMonitor.NotifyService
{
	public static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddNotifier(this IServiceCollection services, IConfiguration configuration)
		{
            var telegramConfiguration = new TelegramConfiguration();
            configuration.Bind("Telegram", telegramConfiguration);
            services.AddSingleton(telegramConfiguration);
            //services.AddScoped<INotifier, Notifier>();
            //services.AddScoped<ITelegramNotifier, TelegramNotifier>();
            services.AddScoped<INotifier, TelegramNotifier>();

            return services;
        }
	}
}