using CurrencyMonitor.Core.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyMonitor.EFDataAccess
{
	public static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
		{
            services
                .AddDbContext<CurrencyDbContext>(options =>
                {
                    var context = new CurrencyDbContext();
                    var contextSchema = context.Schema;

                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, contextSchema));
                })
                .AddScoped<DbContext, CurrencyDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
	}
}