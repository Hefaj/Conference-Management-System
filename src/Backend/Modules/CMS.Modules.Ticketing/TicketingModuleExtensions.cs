using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CMS.Modules.Ticketing.Infrastructure;

namespace CMS.Modules.Ticketing;

public static class TicketingModuleExtensions
{
    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<TicketingDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // WAŻNE: Tu definiujesz, że ten moduł używa konkretnego schematu migracji
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "ticketing");
            });
        });

        // 2. Rejestracja serwisów domenowych (jeśli masz)
        // services.AddScoped<ITicketingService, TicketingService>();

        // 3. Rejestracja Background Workers (jeśli moduł ma swoje tło)
        // services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}