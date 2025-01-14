using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Client;

public class GetClientByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]IClient context
    )
    {
        if(!Guid.TryParse(id, out var Id)) return Results.NotFound();
        
        var client = await context.FindByIdAsync(Id);
        
        return Results.Ok(client);
    }
}