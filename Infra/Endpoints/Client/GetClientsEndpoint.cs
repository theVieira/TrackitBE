using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Infra.Persistence;

namespace Trackit.Endpoints.Client;

public abstract class GetClientsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromQuery]int skip,
        [FromQuery]int take,
        [FromServices]AppDbContext context
    )
    {
        var clients = context.Clients
            .Skip(skip)
            .Take(take)
            .Include(c => c.Avatar)
            .ToListAsync();

        var total = context.Clients.CountAsync();

        await Task.WhenAll(clients, total);
         
        var response = new { Total = total, Items = clients };
        return Results.Ok(response);
    }
}