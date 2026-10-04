using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.infrastructure.Persistence.Configurations;

internal class TierlistEntryConfig : IEntityTypeConfiguration<TierlistEntry>
{
    public void Configure(EntityTypeBuilder<TierlistEntry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .HasMaxLength(50);
    }
}
