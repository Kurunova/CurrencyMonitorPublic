using CurrencyMonitor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyMonitor.EFDataAccess.Configurations
{
    public class TelegramSettingConfiguration : IEntityTypeConfiguration<TelegramSetting>
    {
        public void Configure(EntityTypeBuilder<TelegramSetting> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.ChatId).IsRequired();
            builder.Property(p => p.Subscribe).IsRequired();
            builder.Property(p => p.DateCreated).IsRequired();
            builder.Property(p => p.DateCreated);
        }
    }
}