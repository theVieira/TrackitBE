using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Client;

public class GetClientByNameEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{name}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string name,
        [FromServices]AppDbContext context
    )
    {
        var nameLower = name.ToLower();
        
        var client = await context
            .Clients
            .Where(x => x.Name.ToLower() == nameLower)
            .Include(c  => c.Avatar)
            .ToListAsync();
        
        return Results.Ok();
    }
}