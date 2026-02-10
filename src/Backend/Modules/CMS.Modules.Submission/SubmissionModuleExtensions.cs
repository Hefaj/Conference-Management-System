using CMS.Modules.Submission.Domain;
using CMS.Modules.Submission.Domain.Repositories;
using CMS.Modules.Submission.Infrastructure;
using CMS.Modules.Submission.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Modules.Submission;

public static class SubmissionModuleExtensions
{
    public static IServiceCollection AddSubmissionModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<SubmissionDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // WAŻNE: Tu definiujesz, że ten moduł używa konkretnego schematu migracji
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "submission");
            });
        });

        // 2. Rejestracja serwisów domenowych (jeśli masz)
        // services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<ISubmissionUnitOfWork, SubmissionUnitOfWork>();
        services.AddScoped<IAbstractRepository, AbstractRepository>();

        // 3. Rejestracja Background Workers (jeśli moduł ma swoje tło)
        // services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}