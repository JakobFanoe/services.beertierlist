using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using services.beertierlist.application.Commands.AddTier;
using services.beertierlist.application.Commands.AddTierlistEntry;
using services.beertierlist.application.Commands.AddWheelOptions;
using services.beertierlist.application.Commands.RemoveTier;
using services.beertierlist.application.Commands.RemoveTierlistEntry;
using services.beertierlist.application.Commands.RemoveWheelOption;
using services.beertierlist.application.Queries.TierlistEntries;
using services.beertierlist.application.Queries.Tiers;
using services.beertierlist.application.Queries.WheelOptions;
using services.beertierlist.application.Services;
using services.beertierlist.domain.Interfaces;
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
        builder.Services.AddScoped<IAddTierlistEntryCommandHandler, AddTierlistEntryCommandHandler>();
        builder.Services.AddScoped<IRemoveTierlistEntryCommandHandler, RemoveTierlistEntryCommandHandler>();
        builder.Services.AddScoped<ITierlistEntriesQueryHandler, TierlistEntriesQueryHandler>();

        builder.Services.AddScoped<ITierlistEntryRepository, TierlistEntryRepository>();

        // Wheel options
        builder.Services.AddScoped<IAddWheelOptionsCommandHandler, AddWheelOptionsCommandHandler>();
        builder.Services.AddScoped<IRemoveWheelOptionCommandHandler, RemoveWheelOptionCommandHandler>();
        builder.Services.AddScoped<IWheelOptionsQueryHandler, WheelOptionsQueryHandler>();

        builder.Services.AddScoped<IWheelOptionRepository, WheelOptionRepository>();


        // Infrastructure
        builder.Services.AddDbContext<BeerTierlistDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("Database")
            ));
        builder.Services.AddScoped<IBeertierlistDbContext>(serviceProvider =>
            serviceProvider.GetRequiredService<BeerTierlistDbContext>());

        builder.Services.AddAzureClients(azure =>
        {
            azure.AddBlobServiceClient(
                builder.Configuration.GetConnectionString("Blobstorage"));
        });

        builder.Services.AddScoped<IImageService, ImageService>();

        return builder;
    }
}
