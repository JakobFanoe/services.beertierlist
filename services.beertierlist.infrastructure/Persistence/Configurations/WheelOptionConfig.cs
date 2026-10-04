using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.infrastructure.Persistence.Configurations;

internal class WheelOptionConfig : IEntityTypeConfiguration<WheelOption>
{
    public void Configure(EntityTypeBuilder<WheelOption> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .HasMaxLength(50);
    }
}
