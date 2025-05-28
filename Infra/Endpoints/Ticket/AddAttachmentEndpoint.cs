using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Application.Services;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class AddAttachmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("attachment", HandleAsync)
            .DisableAntiforgery();

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleAsync(
        [FromForm]AddAttachmentRequest request,
        [FromServices]AppDbContext context,
        TokenService tokenService,
        HttpContext httpContext,
        IConfiguration configuration,
        FileService fileService
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);

        var tech = await context.Techs.FindAsync(techId);
        if(tech == null) return Results.Unauthorized();
        
        if(!Guid.TryParse(request.TicketId, out var ticketId)) return Results.BadRequest();
        
        var ticket = await context.Tickets.FindAsync(ticketId);
        if(ticket is null) return Results.NotFound("Ticket not found");

        var filename = $"{ticket.SmallId}-{request.File.FileName}";

        var path = "Attachments/Tickets";
        var url = configuration["UploadConfig:Url"];
        
        var attachment = TicketAttachment.Factory.Create(
            tech.Id, ticket.Id, filename, request.File.Length, path, $"{url}/attachments/{filename}"
        );
        
        ticket.AddAttachment(attachment);
        context.Tickets.Update(ticket);
        await context.Attachments.AddAsync(attachment);
        await context.SaveChangesAsync();
        
        await fileService.AttachFile(request.File, path);
        
        return Results.Ok();
    }
}

public class AddAttachmentRequest
{
    [Required]
    [FromForm(Name = "file")]
    public required IFormFile File { get; set; }
    public required string TicketId { get; set; }
}