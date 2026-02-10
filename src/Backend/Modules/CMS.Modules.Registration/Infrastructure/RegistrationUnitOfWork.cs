using CMS.Modules.Registration.Domain;

namespace CMS.Modules.Registration.Infrastructure;

internal class RegistrationUnitOfWork : IRegistrationUnitOfWork
{
    private readonly AttendeeDbContext _dbContext;

    public RegistrationUnitOfWork(AttendeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
