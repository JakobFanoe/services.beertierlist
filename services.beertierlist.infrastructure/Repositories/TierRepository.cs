using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Interfaces;
using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.infrastructure.Repositories;

public class TierRepository(IBeertierlistDbContext dbContext) : ITierRepository
{
    public async Task AddTier(Tier tier, CancellationToken cancellationToken)
    {
        dbContext.Tiers.Add(tier);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Tier>> GetTiers(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.Tiers
            .Where(tier => tier.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveTier(string userId, Guid id, CancellationToken cancellationToken)
    {
        var tier = await dbContext.Tiers.FirstOrDefaultAsync(tier => tier.UserId == userId && tier.Id == id, cancellationToken);

        if (tier != null)
        {
            dbContext.Tiers.Remove(tier);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
