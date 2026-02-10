using CMS.Modules.Registration.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Registration.Infrastructure;

internal class AttendeeDbContext : DbContext
{
    public AttendeeDbContext(DbContextOptions<AttendeeDbContext> options) : base(options)
    {
    }

    public DbSet<Attendee> Attendees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("submission");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
