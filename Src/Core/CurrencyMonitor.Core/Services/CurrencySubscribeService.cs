using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyMonitor.Core.Contracts;
using CurrencyMonitor.Core.Contracts.DataAccess;
using CurrencyMonitor.Core.Entities;

namespace CurrencyMonitor.Core.Services
{
    public class CurrencySubscribeService : ICurrencySubscribeService
    {
        private readonly IRepository<TelegramSetting> _repository;

        public CurrencySubscribeService(IRepository<TelegramSetting> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TelegramSetting> GetSubscribes()
        {
            var found = _repository.Find(p => p.Subscribe);
            return found;
        }

        public void Subscribe(long chatId)
        {
            var found = _repository.Find(p => p.ChatId == chatId).ToList();

            if (!found.Any())
            {
                var settings = new TelegramSetting{ ChatId = chatId, Subscribe = true, DateCreated = DateTime.Now };
                _repository.Add(settings);
            }
            else
            {
                var settings = found.LastOrDefault();
                settings.Subscribe = true;
                settings.DateUpdated = DateTime.Now;
                _repository.Update(settings);
            }
        }

        public void UnSubscribe(long chatId)
        {
            var found = _repository.Find(p => p.ChatId == chatId).LastOrDefault();

            if (found == null)
            {
                var settings = new TelegramSetting { ChatId = chatId, Subscribe = false, DateCreated = DateTime.Now };
                _repository.Add(settings);
            }
            else
            {
                found.Subscribe = false;
                found.DateUpdated = DateTime.Now;
                _repository.Update(found);
            }
        }
    }
}