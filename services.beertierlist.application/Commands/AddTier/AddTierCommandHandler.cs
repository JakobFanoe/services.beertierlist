using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.domain.Tierlist;

namespace services.beertierlist.application.Commands.AddTier;

public class AddTierCommandHandler(ITierRepository tierRepository) : IAddTierCommandHandler
{
    public Task Handle(AddTierCommand command, CancellationToken cancellationToken)
    {
        var tier = new Tier(Guid.CreateVersion7(), command.UserId, command.Name);

        return tierRepository.AddTier(tier, cancellationToken);
    }
}
