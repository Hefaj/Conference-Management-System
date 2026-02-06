using CMS.Modules.Cfp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Modules.Cfp.Infrastructure.Configurations;

internal class ConferenceConfiguration : IEntityTypeConfiguration<Conference>
{
    public void Configure(EntityTypeBuilder<Conference> builder)
    {
        builder.HasKey(c => c.Id);
        builder.OwnsOne(s => s.Speaker);
    }
}
