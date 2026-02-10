using CMS.Modules.Submission.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Modules.Submission.Infrastructure.Configurations;

internal class AbstractConfiguration : IEntityTypeConfiguration<Abstract>
{
    public void Configure(EntityTypeBuilder<Abstract> builder)
    {
        builder.HasKey(c => c.Id);
    }
}
