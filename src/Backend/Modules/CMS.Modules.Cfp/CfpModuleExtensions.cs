using CMS.Modules.Cfp.Domain;
using CMS.Modules.Cfp.Domain.Repositories;
using CMS.Modules.Cfp.Infrastructure;
using CMS.Modules.Cfp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Modules.Cfp;

public static class CfpModuleExtensions
{
    public static IServiceCollection AddCfpModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<CfpDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // WAŻNE: Tu definiujesz, że ten moduł używa konkretnego schematu migracji
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "cfp");
            });
        });

        // 2. Rejestracja serwisów domenowych (jeśli masz)
        // services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<ICfpUnitOfWork, CfpUnitOfWork>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();

        // 3. Rejestracja Background Workers (jeśli moduł ma swoje tło)
        // services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}