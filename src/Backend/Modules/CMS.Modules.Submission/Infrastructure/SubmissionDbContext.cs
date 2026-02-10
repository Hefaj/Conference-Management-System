using CMS.Modules.Submission.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Modules.Submission.Infrastructure;

internal class SubmissionDbContext : DbContext
{
    public SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : base(options)
    {
    }

    public DbSet<Abstract> Conferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("submission");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
