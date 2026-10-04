using Microsoft.EntityFrameworkCore;
using services.beertierlist.domain.Interfaces;
using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Wheel;

namespace services.beertierlist.infrastructure.Repositories;

public class WheelOptionRepository(IBeertierlistDbContext dbContext) : IWheelOptionRepository
{
    public async Task AddOptions(IReadOnlyList<WheelOption> options, CancellationToken cancellationToken)
    {
        dbContext.WheelOptions.AddRange(options);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WheelOption>> GetOptions(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.WheelOptions
            .Where(option => option.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveOption(Guid id, string userId, CancellationToken cancellationToken)
    {
        var option = await dbContext.WheelOptions.FirstOrDefaultAsync(option => option.UserId == userId && option.Id == id, cancellationToken);

        if (option != null)
        {
            dbContext.WheelOptions.Remove(option);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
