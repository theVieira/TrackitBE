using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Application.Services;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Authentication;

public class SignInEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]AuthenticationRequest request,
        [FromServices]AppDbContext context,
        TokenService tokenService
    )
    {
        var tech = await context.Techs.SingleOrDefaultAsync(x => x.Email == request.Email);
        
        if(tech == null || !tech.CheckPassword(request.Password))
            return Results.Unauthorized();

        var token = tokenService.GenerateToken(tech);
        
        return Results.Ok(new { token, tech });
    }
}

public record AuthenticationRequest(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    string Email,
    
    [Required(ErrorMessage = "Password is required")]
    string Password
);