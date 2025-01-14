using Trackit.Domain.Entities;

namespace Trackit.Authentication;

public interface ITokenManager
{
    string GenerateToken(Tech tech);
    string GenerateRefreshToken(Tech tech);
    string GetClaimValue(string token, string claimType);
}