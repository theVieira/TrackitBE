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
        => app.MapPost("avatar", HandleAsync);

    private static async Task<IResult> HandleAsync(
      
    )
    {
        
        
        return Results.Ok();
    }
}

//  [FromForm]IFormFile file,
//       [FromServices]ITokenManager tokenManager,
//       [FromServices]ITech techContext,
//       [FromServices]IAvatar avatarContext

//var authorizationHeader = HttpRequestHeader.Authorization.ToString();
// var authorizationToken = authorizationHeader.Split("Bearer ");

// var tokenClaim = tokenManager.GetClaimValue(JwtRegisteredClaimNames.Sub, authorizationToken[1]);
// 
// if(!Guid.TryParse(tokenClaim, out var id)) return Results.Unauthorized();

// var tech = await techContext.FindByIdAsync(id);
// 
// if(tech is null) return Results.Unauthorized();
// 
// var avatar = Avatar.Factory.Create(Settings.UploadUrl, file.FileName, Settings.UploadPath);
// 
// await using (var fileStream = new FileStream(Settings.UploadPath, FileMode.Create))
// {
//     await file.CopyToAsync(fileStream);
// }
// 
// await avatarContext.AddAsync(avatar);