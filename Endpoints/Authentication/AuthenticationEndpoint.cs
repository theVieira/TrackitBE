using Microsoft.AspNetCore.Mvc;
using Trackit.Authentication;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Authentication;

public class AuthenticationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]AuthenticationRequest request,
        [FromServices]ITokenManager tokenManager,
        [FromServices]ITech context
    )
    {
        var tech = await context.FindByEmailAsync(request.Email);
        
        if(tech == null)
            return Results.Unauthorized();

        if(!tech.CheckPassword(request.Password))
           return Results.Unauthorized();

        var token = tokenManager.GenerateToken(tech);
        
        return Results.Ok(new { token, tech });
    }
}

public record AuthenticationRequest(string Email, string Password);