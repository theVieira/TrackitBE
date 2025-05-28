using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Entities;
using Trackit.Infra.Persistence;

namespace Trackit.Endpoints.Client;

public abstract class CreateClientEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]CreateClientRequest request,
        [FromServices]AppDbContext context
    )
    {
        var client = Domain.Entities.Client.Factory.Create(
            request.Name,
            request.Cnpj,
            request.Email,
            request.Phone,
            request.Tag
        );
        
        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();

        return Results.Created("Client", client);
    }
}

public record CreateClientRequest(
    string Name,
    string Cnpj,
    string Email,
    string Phone,
    eClientTag Tag
);