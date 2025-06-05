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
        [FromQuery]string? clientName,
        [FromServices]AppDbContext context
    )
    {
        var query = context.Clients.AsQueryable();

        query
            .OrderBy(c => c.Name)
            .Skip(skip)
            .Take(take)
            .Include(c => c.Avatar);
        
        if(!string.IsNullOrEmpty(clientName)) query = query.Where(c => c.Name == clientName);
        
        var clients = await query.ToListAsync();

        var total = await query.CountAsync();
         
        return Results.Ok(new { Total = total, Items = clients });
    }
}