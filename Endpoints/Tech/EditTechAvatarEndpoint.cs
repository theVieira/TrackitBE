using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Tech;

public class EditTechAvatarEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("avatar", HandleAsync)
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithName("EditTechAvatar")
            .WithTags("Techs");

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleAsync(
        [FromForm]EditTechAvatarDto request,
        [FromServices]ITokenManager tokenManager,
        [FromServices]ITech techContext,
        [FromServices]IAvatar avatarContext,
        HttpContext httpContext
    )
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
        var authorizationToken = authorizationHeader.Split("Bearer ");

        var tokenClaim = tokenManager.GetClaimValue(JwtRegisteredClaimNames.Sub, authorizationToken[1]);
 
        if(!Guid.TryParse(tokenClaim, out var id)) return Results.Unauthorized();

        var tech = await techContext.FindByIdAsync(id);

        if(tech is null) return Results.Unauthorized();

        var avatar = Avatar.Factory.Create(Settings.UploadUrl, request.File.FileName, Settings.UploadPath);

        await using (var fileStream = new FileStream(Settings.UploadPath, FileMode.Create))
        {
            await request.File.CopyToAsync(fileStream);
        }

        await avatarContext.AddAsync(avatar);
        
        return Results.Ok();
    }
}

public class EditTechAvatarDto
{
    [FromForm]
    public required IFormFile File { get; set; }
}
