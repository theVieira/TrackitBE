using Microsoft.AspNetCore.Mvc;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.File;

public class DownloadFileEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("attachment/{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]AppDbContext context
    )
    {
        if (!Guid.TryParse(id, out var attachmentId))
            return Results.Unauthorized();
        
        var file = await context.Attachments.FindAsync(attachmentId);
        if(file is null) return Results.NotFound();
        
        return Results.File(
            System.IO.File.OpenRead(file.Path),
            "application/octet-stream",
            file.Filename,
            enableRangeProcessing: true
        );
    }
}