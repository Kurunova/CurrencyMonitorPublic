using System;
using CurrencyMonitor.Core.Contracts.DataAccess;

namespace CurrencyMonitor.Core.Entities
{
    public class TelegramSetting : IEntity
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public bool Subscribe { get; set; }
        
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}