using CMS.Modules.Cfp.Domain.Models;
using CMS.Modules.Cfp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Cfp.Infrastructure.Repositories;

internal class ConferenceRepositonry : IConferenceRepositonry
{
    private readonly CfpDbContext _dbContext;

    public ConferenceRepositonry(CfpDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Conference Obj)
    {
        await _dbContext.Conferences.AddAsync(Obj);
    }

    public async Task AddManyAsync(List<Conference> Objs)
    {
        await _dbContext.Conferences.AddRangeAsync(Objs);
    }

    public async Task<Conference?> GetAsync(Guid Id)
    {
        return await _dbContext.Conferences.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<IEnumerable<Conference>?> GetManyAsync(List<Guid> Ids)
    {
        return await _dbContext.Conferences.Where(x => Ids.Contains(x.Id)).ToListAsync();
    }
}
