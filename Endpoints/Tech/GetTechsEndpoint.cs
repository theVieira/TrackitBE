using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Tech;

public class GetTechsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromQuery]int? skip,
        [FromQuery]int? take,
        [FromServices]ITech context
    )
    {
        var techs = await context.ListAsync(skip ?? 0, take ?? 10);
        
        return Results.Ok(techs);
    }
}