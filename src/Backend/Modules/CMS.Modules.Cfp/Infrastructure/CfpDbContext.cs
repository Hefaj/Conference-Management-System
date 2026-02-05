using CMS.Modules.Cfp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Cfp.Infrastructure;

internal class CfpDbContext : DbContext
{
    public CfpDbContext(DbContextOptions<CfpDbContext> options) : base(options)
    {
    }

    public DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cfp");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
