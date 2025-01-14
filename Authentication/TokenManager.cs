using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Trackit.Domain.Entities;

namespace Trackit.Authentication;

public class TokenManager(IConfiguration configuration) : ITokenManager
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(Tech tech)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, tech.Id.ToString()),
            new (ClaimTypes.Email, tech.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (ClaimTypes.Role, tech.Role.ToString()),
            new (ClaimTypes.Name, tech.Name)
        };
        
        var expiration = jwtSettings.GetValue<int>("TokenExpiration");

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(expiration),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(Tech tech)
    {
        return "";
    }

    public string GetClaimValue(string token, string claimType)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
        return claim?.Value ?? string.Empty;
    }
}