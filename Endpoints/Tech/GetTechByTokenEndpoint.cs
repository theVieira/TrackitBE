using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Trackit.Authentication;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Tech;

public class GetTechByTokenEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/me", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromServices]ITech context,
        [FromServices]ITokenManager tokenManager
    )
    {
        var authorizationHeader = HttpRequestHeader.Authorization.ToString();
        
        var authorizationToken = authorizationHeader.Split("Bearer ");
        
        var tokenClaim = tokenManager.GetClaimValue(JwtRegisteredClaimNames.Sub, authorizationToken[1]);

        if (!Guid.TryParse(tokenClaim, out var id)) return Results.Unauthorized();
        
        var tech = await context.FindByIdAsync(id);
            
        return Results.Ok(tech);
    }
}