using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.infrastructure.Persistence.Configurations;

internal class TierConfig : IEntityTypeConfiguration<Tier>
{
    public void Configure(EntityTypeBuilder<Tier> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .HasMaxLength(50);
    }
}
