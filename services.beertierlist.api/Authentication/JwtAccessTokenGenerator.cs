using Microsoft.IdentityModel.Tokens;
using services.beertierlist.application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace services.beertierlist.api.Authentication;

public class JwtAccessTokenGenerator(JwtSettings jwtSettings) : IAccessTokenGenerator
{
    public string Generate(string userId, string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username)
        };

        var credentials = new SigningCredentials(jwtSettings.SigningKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(jwtSettings.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}