using Microsoft.IdentityModel.Tokens;

namespace services.beertierlist.api.Authentication;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";

    private JwtSettings(string issuer, string audience, SymmetricSecurityKey signingKey)
    {
        Issuer = issuer;
        Audience = audience;
        SigningKey = signingKey;
    }

    public string Issuer { get; }
    public string Audience { get; }
    public SymmetricSecurityKey SigningKey { get; }
    public TimeSpan TokenLifetime { get; } = TimeSpan.FromMinutes(15);

    public static JwtSettings Load(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        var issuer = section["Issuer"];
        var audience = section["Audience"];
        var encodedKey = section["Key"];

        if (string.IsNullOrWhiteSpace(issuer))
            throw new InvalidOperationException("Jwt:Issuer must be configured.");

        if (string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("Jwt:Audience must be configured.");

        if (string.IsNullOrWhiteSpace(encodedKey))
            throw new InvalidOperationException("Jwt:Key must be configured as a Base64-encoded random key.");

        byte[] keyBytes;
        try
        {
            keyBytes = Convert.FromBase64String(encodedKey);
        }
        catch (FormatException exception)
        {
            throw new InvalidOperationException("Jwt:Key must be a Base64-encoded random key.", exception);
        }

        if (keyBytes.Length < 32)
            throw new InvalidOperationException("Jwt:Key must contain at least 32 random bytes.");

        return new JwtSettings(issuer, audience, new SymmetricSecurityKey(keyBytes));
    }
}
