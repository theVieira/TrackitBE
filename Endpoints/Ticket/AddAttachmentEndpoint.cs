using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Ticket;

public class AddAttachmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("attachment", HandleAsync)
            .DisableAntiforgery();

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleAsync(
        HttpContext context,
        [FromForm]AddAttachmentRequest request,
        [FromServices]ITokenManager tokenManager,
        [FromServices]ITicket ticketContext,
        [FromServices]IAttachment attachmentContext
    )
    {
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();
        if(string.IsNullOrWhiteSpace(authorizationHeader)) return Results.Unauthorized();
        
        var authorizationToken = authorizationHeader.Split("Bearer ")[1];
        var id = tokenManager.GetClaimValue(authorizationToken, ClaimTypes.NameIdentifier);
        
        if(!Guid.TryParse(id, out Guid techId)) return Results.Unauthorized();

        if(!Guid.TryParse(request.TicketId, out Guid ticketId)) return Results.Unauthorized();
        
        var findTicket = await ticketContext.FindByIdAsync(ticketId);
        if(findTicket is null) return Results.NotFound("Ticket not found");

        var filename = $"{findTicket.SmallId}-{request.File.FileName}";

        var directory = Path.Combine(Directory.GetCurrentDirectory(), Settings.UploadPath, "Attachments");
        
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        
        var path = Path.Combine(directory, filename);
        
        var attachment = Attachment.Factory.Create(
            findTicket.Id, techId, request.File.FileName, request.File.Length, path, $"{Settings.UploadPath}/attachments/{filename}"     
        );
        
        await attachmentContext.AddAsync(attachment);

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }
        
        return Results.Ok();
    }
}

public class AddAttachmentRequest
{
    [Required]
    public required IFormFile File { get; set; }
    public required string TicketId { get; set; }
}