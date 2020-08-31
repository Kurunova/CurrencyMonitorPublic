using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CurrencyMonitor.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CurrencyMonitor.Application
{
    public class Program
    {
        private static ILogger logger;

        public static async Task Main(string[] args)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();

                logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.File($"Logs\\{DateTime.Today.Date:yyyy-MM-dd}.txt")
                    .CreateLogger();

                var serviceProvider = new ServiceCollection()
                    .AddApplication(configuration)
                    .BuildServiceProvider();

                logger.Error("wer");

                var currencyExternalService = serviceProvider.GetService<ICurrencyExternalService>();
                var currencyDataService = serviceProvider.GetService<ICurrencyDataService>();
                var currencyAnalysisService = serviceProvider.GetService<ICurrencyAnalysisService>();
                var currencySubscribeService = serviceProvider.GetService<ICurrencySubscribeService>();
                var notifier = serviceProvider.GetService<INotifier>();

                var currencyType = "RUB";
                var currencyTypes = new List<string> { "RUB", "USD", "EUR", "CNY" };

                var startDate = DateTime.Today.AddMonths(-1);
                var endDate = DateTime.Today;
                
                //notifier.Start();

                LoadPeriodHistory(currencyExternalService, currencyDataService, startDate, endDate.AddDays(-1));
                LoadLastData(currencyExternalService, currencyDataService);

                var allPeriodHistory = currencyDataService.GetData(startDate, endDate);
                var history = allPeriodHistory.Where(p => p.Type == currencyType).ToList();
                var today = history.Where(p => p.RatingDate == DateTime.Today);

                var message = currencyAnalysisService.Verdict(currencyType, today, history);

                if (!string.IsNullOrEmpty(message))
                {
                    var subscribes = currencySubscribeService.GetSubscribes().ToList();
                    var subscribesChatId = subscribes.Select(p => p.ChatId);

                    foreach (var chatId in subscribesChatId)
                    {
                        await notifier.Send(chatId, message);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Fatal($"{e.Message}: {e}");
            }
        }

        private static void LoadLastData(
            ICurrencyExternalService currencyExternalService,
            ICurrencyDataService currencyDataService)
        {
            var today = DateTime.Today;
            var found = currencyDataService.GetData(today, today);

            if (!found.Any())
            {
                var lastRate = currencyExternalService.GetLastRate();
                currencyDataService.AddData(DateTime.Today, lastRate);
            }
        }

        private static void LoadPeriodHistory(
            ICurrencyExternalService currencyExternalService,
            ICurrencyDataService currencyDataService, 
            DateTime start, 
            DateTime end)
        {
            var found = currencyDataService.GetData(start, end).ToList();

            for (DateTime day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
            {
                if (found.All(p => p.RatingDate != day))
                {
                    var historical = currencyExternalService.GetHistorical(day);
                    currencyDataService.AddData(day, historical);
                }
            }
        }
    }
}