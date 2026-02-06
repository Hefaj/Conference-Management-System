using CMS.Modules.Cfp.Domain;

namespace CMS.Modules.Cfp.Infrastructure;

internal class CfpUnitOfWork : ICfpUnitOfWork
{
    private readonly CfpDbContext _dbContext;

    public CfpUnitOfWork(CfpDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
