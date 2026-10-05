using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using services.beertierlist.domain.Tierlist;
using services.beertierlist.domain.Wheel;
using services.beertierlist.domain.Authentication;

namespace services.beertierlist.domain.Interfaces;

public interface IBeertierlistDbContext
{
    public DbSet<Tier> Tiers { get; }
    public DbSet<TierlistEntry> TierlistEntries { get; }
    public DbSet<WheelOption> WheelOptions { get; }
    public DbSet<RefreshToken> RefreshTokens { get; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
