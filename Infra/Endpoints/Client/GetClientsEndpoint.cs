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
        var clients = await context.Clients
            .OrderBy(c => c.Name)
            .Skip(skip)
            .Take(take)
            .Include(c => c.Avatar)
            .ToListAsync();

        var total = await context.Clients.CountAsync();

         
        return Results.Ok(new { Total = total, Items = clients });
    }
}