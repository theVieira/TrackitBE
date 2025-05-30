using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Tech;

public class GetTechsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromQuery]int skip,
        [FromQuery]int take,
        [FromServices]AppDbContext context
    )
    {
        var count = await context.Techs.CountAsync();
        
        var techs = await context
            .Techs
            .OrderBy(c => c.Name)
            .Skip(skip)
            .Take(take)
            .Include(t => t.Avatar)
            .ToListAsync();
        
        return Results.Ok(new { Total = count, Items = techs });
    }
}