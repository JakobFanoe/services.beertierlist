using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Interfaces;
using services.beertierlist.domain.Tierlist;
using services.beertierlist.domain.Wheel;
using services.beertierlist.domain.Authentication;

namespace services.beertierlist.infrastructure.Persistence;

public class BeerTierlistDbContext : IdentityDbContext<IdentityUser>, IBeertierlistDbContext
{
    public DbSet<Tier> Tiers { get; set; }
    public DbSet<TierlistEntry> TierlistEntries { get; set; }
    public DbSet<WheelOption> WheelOptions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public BeerTierlistDbContext(
      DbContextOptions<BeerTierlistDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BeerTierlistDbContext).Assembly);
    }
}
