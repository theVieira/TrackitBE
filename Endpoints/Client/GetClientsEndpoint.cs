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
        var response = await context.ListAsync(skip ?? 0, take ?? 20);
        
        var filtredClients = response.Items.Where(x => x.Name == clientName).ToList();
        
        if(!string.IsNullOrWhiteSpace(clientName))
            return Results.Ok(new { Items = filtredClients, Total = response.Total });
            
        
        return Results.Ok(response);
        
    }
}