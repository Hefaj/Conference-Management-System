using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Cfp;

public static class CfpModuleExtensions
{
    // Metoda rozszerzająca interfejs IServiceCollection
    public static IServiceCollection AddCfpModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Rejestracja Bazy Danych (z obsługą schematu dla tego modułu)
        //var connectionString = configuration.GetConnectionString("Database");

        //services.AddDbContext<CfpDbContext>(options =>
        //{
        //    options.UseNpgsql(connectionString, npgsqlOptions =>
        //    {
        //        // WAŻNE: Tu definiujesz, że ten moduł używa konkretnego schematu migracji
        //        npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "cfp");
        //    });
        //});

        // 2. Rejestracja serwisów domenowych (jeśli masz)
        // services.AddScoped<ISubmissionService, SubmissionService>();

        // 3. Rejestracja Background Workers (jeśli moduł ma swoje tło)
        // services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}