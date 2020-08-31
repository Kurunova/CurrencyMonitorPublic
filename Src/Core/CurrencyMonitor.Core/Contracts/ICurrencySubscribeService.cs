using System.Collections.Generic;
using CurrencyMonitor.Core.Entities;

namespace CurrencyMonitor.Core.Contracts
{
    public interface ICurrencySubscribeService
    {
        IEnumerable<TelegramSetting> GetSubscribes();

        void Subscribe(long chatId);

        void UnSubscribe(long chatId);
    }
}