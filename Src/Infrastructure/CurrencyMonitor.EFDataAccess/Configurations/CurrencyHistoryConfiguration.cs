using CurrencyMonitor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyMonitor.EFDataAccess.Configurations
{
    public class CurrencyHistoryConfiguration : IEntityTypeConfiguration<CurrencyHistory>
    {
        public void Configure(EntityTypeBuilder<CurrencyHistory> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Rate).IsRequired();
            builder.Property(p => p.DateCreated).IsRequired();
        }
    }
}