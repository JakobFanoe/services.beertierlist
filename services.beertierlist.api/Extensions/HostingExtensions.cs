using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using services.beertierlist.api.Authentication;
using services.beertierlist.application.Commands.LoginAccount;
using services.beertierlist.application.Commands.RegisterAccount;
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
        // Account
        builder.Services.AddScoped<IRegisterAccountCommandHandler, RegisterAccountCommandHandler>();
        builder.Services.AddScoped<ILoginAccountCommandHandler, LoginAccountCommandHandler>();
        builder.Services.AddScoped<IAccessTokenGenerator, JwtAccessTokenGenerator>();

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

        // Identity
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 12;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
        })
        .AddEntityFrameworkStores<BeerTierlistDbContext>()
        .AddDefaultTokenProviders();

        // JWT Authentication
        var jwtSettings = JwtSettings.Load(builder.Configuration);
        builder.Services.AddSingleton(jwtSettings);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtSettings.SigningKey,
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddAzureClients(azure =>
        {
            azure.AddBlobServiceClient(
                builder.Configuration.GetConnectionString("Blobstorage"));
        });

        builder.Services.AddScoped<IImageService, ImageService>();

        return builder;
    }
}
