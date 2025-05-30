using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Client;

public class GetClientByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]AppDbContext context
    )
    {
        if(!Guid.TryParse(id, out var Id)) return Results.NotFound();
        
        var client = await context.Clients
            .Include(x => x.Avatar)
            .SingleOrDefaultAsync(x => x.Id == Id);
        
        return client is null ? Results.NotFound() : Results.Ok(client);
    }
}