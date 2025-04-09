using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Tech;

public class EditTechAvatarEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("avatar", HandleAsync)
            .DisableAntiforgery();

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleAsync(
        [FromForm]EditTechAvatarRequest request,
        [FromServices]ITokenManager tokenManager,
        [FromServices]ITech techContext,
        [FromServices]IAvatar avatarContext,
        HttpContext httpContext
    )
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
        
        if(string.IsNullOrEmpty(authorizationHeader)) return Results.Unauthorized();
        
        var authorizationToken = authorizationHeader.Split("Bearer ")[1];

        var id = tokenManager.GetClaimValue(authorizationToken, ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out Guid techId)) return Results.Unauthorized();
        
        var tech = await techContext.FindByIdAsync(techId);

        if(tech is null) return Results.Unauthorized();

        var directory = Path.Combine(Directory.GetCurrentDirectory(), Settings.UploadPath, "Avatars");
        
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        var filename = $"{tech.SmallId}-{request.File.FileName}";
        
        var path = Path.Combine(directory, filename);
        
        var avatar = Avatar.Factory.Create($"{Settings.UploadUrl}/avatars/{filename}", filename, path, tech.Id);

        await avatarContext.AddAsync(avatar);
        
        await using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await request.File.CopyToAsync(fileStream);
        }

        return Results.Ok();
    }
}

public class EditTechAvatarRequest
{
    [Required]
    public required IFormFile File { get; set; }
}
