using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Application.Services;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Tech;

public class EditTechAvatarEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("avatar", HandleAsync)
            .DisableAntiforgery();

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleAsync(
        [FromForm]EditTechAvatarRequest request,
        [FromServices]AppDbContext context,
        IConfiguration configuration,
        TokenService tokenService,
        HttpContext httpContext,
        FileService fileService
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);

        var tech = await context.Techs.FindAsync(techId);

        if(tech is null) return Results.Unauthorized();

        var url = configuration["UploadConfig:Url"];

        var path = "/Avatars/Techs";

        var filename = tech.SmallId + "-" + Path.GetFileName(request.File.FileName);
        
        var avatar = TechAvatar.Factory.Create($"{url}/avatars/{filename}", filename, path, tech.Id);

        await context.Avatars.AddAsync(avatar);
        tech.SetAvatar(avatar);
        context.Techs.Update(tech);
        await  context.SaveChangesAsync();

        await fileService.AttachFile(request.File, path);

        return Results.Ok();
    }
}

public class EditTechAvatarRequest
{
    [Required]
    [FromForm(Name = "file")]
    public required IFormFile File { get; set; }
}
