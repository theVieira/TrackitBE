using Microsoft.AspNetCore.Mvc;
using Trackit.Application.Services;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Authentication;

public class AuthenticationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]AuthenticationRequest request,
        [FromServices]AppDbContext context,
        TokenService tokenService
    )
    {
        var tech = await context.Techs.FindAsync(request.Email);
        
        if(tech == null || !tech.CheckPassword(request.Password))
            return Results.Unauthorized();

        var token = tokenService.GenerateToken(tech);
        
        return Results.Ok(new { token, tech });
    }
}

public record AuthenticationRequest(string Email, string Password);