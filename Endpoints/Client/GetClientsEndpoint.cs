using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Client;

public abstract class GetClientsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromQuery]string? clientName,
        [FromQuery]int? skip,
        [FromQuery]int? take,
        [FromServices]IClient context
    )
    {
        var clients = await context.ListAsync(skip ?? 0, take ?? 20);
        
        if(!string.IsNullOrWhiteSpace(clientName))
            return Results.Ok(clients.Where(x => x.Name == clientName).ToList());
        
        return Results.Ok(clients);
        
    }
}