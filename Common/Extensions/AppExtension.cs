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

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapEndpoints();

        app.Run();
    }
}