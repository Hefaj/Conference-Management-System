using CMS.Modules.Ticketing.Domain;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Ticketing.Infrastructure;

internal class TicketingDbContext : DbContext
{
    public TicketingDbContext(DbContextOptions<TicketingDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ticketing");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
