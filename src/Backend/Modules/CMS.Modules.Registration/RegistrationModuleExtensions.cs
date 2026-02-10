using CMS.Modules.Registration.Domain;
using CMS.Modules.Registration.Infrastructure.Repositories;
using CMS.Modules.Registration.Domain.Repositories;
using CMS.Modules.Registration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Modules.Registration;

public static class RegistrationModuleExtensions
{
    public static IServiceCollection AddRegistrationModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<AttendeeDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // WAŻNE: Tu definiujesz, że ten moduł używa konkretnego schematu migracji
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "registration");
            });
        });

        // 2. Rejestracja serwisów domenowych (jeśli masz)
        // services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<IRegistrationUnitOfWork, RegistrationUnitOfWork>();
        services.AddScoped<IAttendeeRepository, AttendeeRepository>();

        // 3. Rejestracja Background Workers (jeśli moduł ma swoje tło)
        // services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}