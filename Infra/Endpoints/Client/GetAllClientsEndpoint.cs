using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Client;

public class GetAllClientsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("all-clients", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromServices]AppDbContext context
    )
    {
        var clients = await context.Clients
            .ToListAsync();
        
        return Results.Ok(clients);
    }
}