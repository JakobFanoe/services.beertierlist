namespace services.beertierlist.api.Configuration;

public static class CorsOriginConfiguration
{
    public static string[] GetAllowedOrigins(IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()?
            .Where(origin => !string.IsNullOrWhiteSpace(origin))
            .Select(origin => origin.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray() ?? [];

        if (allowedOrigins.Length == 0)
            throw new InvalidOperationException("Configure at least one origin in Cors:AllowedOrigins.");

        return allowedOrigins;
    }
}
