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
    string Name,
    string Password,
    string Phone,
    string Email,
    eTechRole ETechRole
);
