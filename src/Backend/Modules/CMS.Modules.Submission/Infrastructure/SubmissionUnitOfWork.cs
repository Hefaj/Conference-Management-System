using CMS.Modules.Submission.Domain;

namespace CMS.Modules.Submission.Infrastructure;

internal class SubmissionUnitOfWork : ISubmissionUnitOfWork
{
    private readonly SubmissionDbContext _dbContext;

    public SubmissionUnitOfWork(SubmissionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
