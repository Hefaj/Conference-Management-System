using CMS.Modules.Registration.Domain.Models;
using CMS.Modules.Registration.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Registration.Infrastructure.Repositories;

internal class AttendeeRepository : IAttendeeRepository
{
    private readonly AttendeeDbContext _dbContext;

    public AttendeeRepository(AttendeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Attendee Obj)
    {
        await _dbContext.Attendees.AddAsync(Obj);
    }

    public async Task AddManyAsync(List<Attendee> Objs)
    {
        await _dbContext.Attendees.AddRangeAsync(Objs);
    }

    public async Task<Attendee?> GetAsync(Guid Id)
    {
        return await _dbContext.Attendees.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<IEnumerable<Attendee>?> GetManyAsync(List<Guid> Ids)
    {
        return await _dbContext.Attendees.Where(x => Ids.Contains(x.Id)).ToListAsync();
    }
}
