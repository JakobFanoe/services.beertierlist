using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Interfaces;
using services.beertierlist.domain.Tierlist;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.infrastructure.Persistence;

public class BeerTierlistDbContext : DbContext, IBeertierlistDbContext
{
    public DbSet<Tier> Tiers { get; set; }
    public DbSet<TierlistEntry> TierlistEntries { get; set; }
    public DbSet<WheelOption> WheelOptions { get; set; }

    public BeerTierlistDbContext()
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BeerTierlistDbContext).Assembly);
    }
}
