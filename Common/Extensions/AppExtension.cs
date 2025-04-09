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

        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(uploadDirectory)) Directory.CreateDirectory(uploadDirectory);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadDirectory),
            RequestPath = "/Uploads"
        });

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapEndpoints();

        app.Run();
    }
}