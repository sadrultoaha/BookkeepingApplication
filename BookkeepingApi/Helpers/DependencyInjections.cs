using BookkeepingApi.Repository;
using BookkeepingApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookkeepingApi.Helpers
{
    public static class DependencyInjections
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPredefinedRecordsRepository, PredefinedRecordsRepository>();
            services.AddScoped<IPredefinedRecordsService, PredefinedRecordsService>();

            services.AddScoped<IReconciliationRecordsRepository, ReconciliationRecordsRepository>();
            services.AddScoped<IReconciliationRecordsService, ReconciliationRecordsService>();
        }
    }
}
