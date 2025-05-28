using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Trackit.Domain.Entities;

namespace Trackit.Authentication;

public class TokenService(IConfiguration configuration)
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

    public Guid GetClaimValueFromRequest(string claimType, HttpContext httpContext)
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
        
        if(string.IsNullOrEmpty(authorizationHeader)) throw new UnauthorizedAccessException();
        
        var authorizationToken = authorizationHeader.Split("Bearer ")[1];
        
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(authorizationToken);
        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
        
        if(claim is null || !Guid.TryParse(claim.Value, out var id)) throw new UnauthorizedAccessException();

        return id;
    }
}