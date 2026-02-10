using CMS.Modules.Submission.Domain.Models;
using CMS.Modules.Submission.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Submission.Infrastructure.Repositories;

internal class AbstractRepository : IAbstractRepository
{
    private readonly SubmissionDbContext _dbContext;

    public AbstractRepository(SubmissionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Abstract Obj)
    {
        await _dbContext.Conferences.AddAsync(Obj);
    }

    public async Task AddManyAsync(List<Abstract> Objs)
    {
        await _dbContext.Conferences.AddRangeAsync(Objs);
    }

    public async Task<Abstract?> GetAsync(Guid Id)
    {
        return await _dbContext.Conferences.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<IEnumerable<Abstract>?> GetManyAsync(List<Guid> Ids)
    {
        return await _dbContext.Conferences.Where(x => Ids.Contains(x.Id)).ToListAsync();
    }
}
