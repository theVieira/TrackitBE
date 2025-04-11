using Microsoft.Extensions.FileProviders;
using Trackit.Common.Extensions.Endpoints;

namespace Trackit.Common.Extensions;

public static class AppExtension
{
    public static void AddAppConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("DevelopmentPolicy");
        }
        
        app.UseHttpsRedirection();

        var avatarUploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Avatars");
        if (!Directory.Exists(avatarUploadDirectory)) Directory.CreateDirectory(avatarUploadDirectory);
        
        var attachmentUploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Attachments");
        if (!Directory.Exists(attachmentUploadDirectory)) Directory.CreateDirectory(attachmentUploadDirectory);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(avatarUploadDirectory),
            RequestPath = "/uploads/avatars"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(attachmentUploadDirectory),
            RequestPath = "/uploads/attachments"
        });
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapEndpoints();

        app.Run();
    }
}