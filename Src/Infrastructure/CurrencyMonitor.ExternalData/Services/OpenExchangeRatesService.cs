using System;
using System.Collections.Generic;
using System.Net.Http;
using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.ExternalData.Configurations;
using CurrencyMonitor.ExternalData.InternalModels;
using CurrencyMonitor.ExternalData.Models;
using Newtonsoft.Json;

namespace CurrencyMonitor.ExternalData.Services
{
    public class OpenExchangeRatesService : ICurrencyExternalService
    {
        private readonly string _host;
        private readonly string _appId;

        public OpenExchangeRatesService(OpenExchangeRatesServiceConfiguration configuration)
        {
            _host = configuration.Host;
            _appId = configuration.ApiId;
        }

        public IList<ICurrencyData> GetLastRate()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{_host}/api/latest.json?app_id={_appId}");

            var latestRate = JsonConvert.DeserializeObject<LatestRate>(response.Result);

            var result = new List<ICurrencyData>();
            foreach (var key in latestRate.Rates.Keys) 
            {
                if (latestRate.Rates.TryGetValue(key, out double value)) 
                {
                    result.Add(new CurrencyData()
                    {
                        Type = key,
                        Rate = value
                    });
                }
            }
            
            return result;
        }

        public IList<ICurrencyData> GetHistorical(DateTime date)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{_host}/api/historical/{date:yyyy-MM-dd}.json?app_id={_appId}");

            var latestRate = JsonConvert.DeserializeObject<LatestRate>(response.Result);

            var result = new List<ICurrencyData>();
            foreach (var key in latestRate.Rates.Keys)
            {
                if (latestRate.Rates.TryGetValue(key, out double value))
                {
                    result.Add(new CurrencyData()
                    {
                        Type = key,
                        Rate = value
                    });
                }
            }

            return result;
        }

        public IList<ICurrencyName> GetCurrencyNameList()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{_host}/api/currencies.json?app_id={_appId}");

            var currencies = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Result);

            var result = new List<ICurrencyName>();
            foreach (var key in currencies.Keys)
            {
                if (currencies.TryGetValue(key, out string value))
                {
                    result.Add(new CurrencyName()
                    {
                        Code = key,
                        Name = value
                    });
                }
            }

            return result;
        }
    }
}