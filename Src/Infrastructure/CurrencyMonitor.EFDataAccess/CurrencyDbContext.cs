using CurrencyMonitor.Core.Contracts.DataAccess;
using CurrencyMonitor.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyMonitor.EFDataAccess
{
    public class CurrencyDbContext : DbContext, IDbContext
    {
	    public CurrencyDbContext(DbContextOptions options)
		    : base(options)
	    {
	    }

	    public CurrencyDbContext()
	    {
	    }

		public DbSet<CurrencyHistory> CurrencyHistories { get; set; }

	    public DbSet<TelegramSetting> TelegramSettings { get; set; }

        public string Schema
        {
            get { return "cur"; }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=ELENAPC;Database=CurrencyMonitor;user id=admin;password='1qaz@WSX'");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema(Schema);
        //    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        //}
	}
}