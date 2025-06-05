using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Application.Services;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Client;

public class EditClientAvatarEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("avatar", HandleAsync)
            .DisableAntiforgery();

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleAsync(
        [FromForm]AddClientAvatarRequest request,
        [FromServices]AppDbContext context,
        FileService fileService,
        IConfiguration configuration
    )
    {
        if (!Guid.TryParse(request.ClientId, out var clientId)) return Results.BadRequest();
            
        var client = await  context.Clients.FindAsync(clientId);
        if (client == null) return Results.NotFound();

        var filename = client.SmallId + "-" + Path.GetFileName(request.File.FileName);
        var path = "/Avatars/Clients/";
        var url = configuration["UploadConfig:Url"];
        
        var avatar = ClientAvatar.Factory.Create(
          $"{url}/avatars/{filename}", filename, path, client.Id
        );

        client.SetAvatar(avatar);
        context.Clients.Update(client);
        await context.Avatars.AddAsync(avatar);
        await context.SaveChangesAsync();
        
        await fileService.AttachFile(request.File, "Avatars/Clients");
        
        return Results.Ok();
    }
}

public record AddClientAvatarRequest(
    [Required(ErrorMessage = "File is required")]
    [FromForm(Name = "file")] 
    IFormFile File,

    [Required(ErrorMessage = "ClientId is required")] 
    string ClientId
);