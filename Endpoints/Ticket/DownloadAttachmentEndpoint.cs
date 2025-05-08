using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Ticket;

public class DownloadAttachmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("attachment/{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]IAttachment attachmentContext,
        HttpContext context
    )
    {
        if (!Guid.TryParse(id, out var attachmentId))
            return Results.Unauthorized();
        
        var file = await attachmentContext.FindByIdAsync(attachmentId);
        if(file is null) return Results.NotFound();
        
        return Results.File(
            System.IO.File.OpenRead(file.Path),
            "application/octet-stream",
            file.Filename,
            enableRangeProcessing: true
        );
    }
}