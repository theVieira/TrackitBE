using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Tech;

public abstract class CreateTechEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]CreateTechRequest request,
        [FromServices]AppDbContext context
    )
    {
        var tech = Domain.Entities.Tech.Factory.Create(
            request.Name,
            request.Password,
            request.Phone,
            request.Email,
            request.ETechRole
        );
        
        await context.Techs.AddAsync(tech);
        await context.SaveChangesAsync();
        
        return Results.Created("Tech", tech);
    }
}

public record CreateTechRequest(
    [Required(ErrorMessage = "Name is required")]
    string Name,
    
    [Required(ErrorMessage = "Password is required")]
    string Password,
    
    [Required(ErrorMessage = "Phone is required")]
    string Phone,
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string Email,
    
    [Required(ErrorMessage = "Role is required")]
    eTechRole ETechRole
);
