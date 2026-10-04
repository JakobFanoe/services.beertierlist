using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Interfaces;
using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.infrastructure.Repositories;

public class TierlistEntryRepository(IBeertierlistDbContext dbContext) : ITierlistEntryRepository
{
    public Task AddTierlistEntry(string userId, TierlistEntry tierlistEntry, CancellationToken cancellationToken)
    {
        dbContext.TierlistEntries.Add(tierlistEntry);
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TierlistEntry>> GetEntries(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.TierlistEntries
            .Where(entry => entry.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TierlistEntry?> GetEntry(Guid id, string userId, CancellationToken cancellationToken)
    {
        return await dbContext.TierlistEntries
            .FirstOrDefaultAsync(entry => entry.UserId == userId && entry.Id == id, cancellationToken: cancellationToken);
    }

    public async Task RemoveTierlistEntry(string userId, Guid tierlistEntryId, CancellationToken cancellationToken)
    {
        var entry = await dbContext.TierlistEntries.FirstOrDefaultAsync(entry => entry.UserId == userId && entry.Id == tierlistEntryId, cancellationToken);

        if (entry != null)
        {
            dbContext.TierlistEntries.Remove(entry);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
