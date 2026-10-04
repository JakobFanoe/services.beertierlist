using Microsoft.EntityFrameworkCore;
using services.beertierlist.application.Commands.AddTier;
using services.beertierlist.application.Commands.RemoveTier;
using services.beertierlist.application.Queries.Tiers;
using services.beertierlist.domain.Interfaces.Repositories;
using services.beertierlist.infrastructure.Persistence;
using services.beertierlist.infrastructure.Repositories;

namespace services.beertierlist.api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Tier
        builder.Services.AddScoped<IAddTierCommandHandler, AddTierCommandHandler>();
        builder.Services.AddScoped<IRemoveTierCommandHandler, RemoveTierCommandHandler>();
        builder.Services.AddScoped<ITiersQueryHandler, TiersQueryHandler>();

        builder.Services.AddScoped<ITierRepository, TierRepository>();

        // Tierlist entries


        // Infrastructure
        builder.Services.AddDbContext<BeerTierlistDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("Database")
            ));

        return builder;
    }
}
