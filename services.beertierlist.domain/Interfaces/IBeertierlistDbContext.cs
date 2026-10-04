using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Tierlist;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.domain.Interfaces;

public interface IBeertierlistDbContext
{
    public DbSet<Tier> Tiers { get; }
    public DbSet<TierlistEntry> TierlistEntries { get; }
    public DbSet<WheelOption> WheelOptions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
