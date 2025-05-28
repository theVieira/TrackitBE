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
        var count =  context.Techs.CountAsync();
        
        var techs =  context
            .Techs
            .Skip(skip)
            .Take(take)
            .Include(t => t.Avatar)
            .ToListAsync();
        
        await Task.WhenAll(techs, count);
        
        return Results.Ok((total: count, items: techs));
    }
}