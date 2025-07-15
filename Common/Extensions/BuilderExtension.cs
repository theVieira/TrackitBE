using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Trackit.Application.Services;
using Trackit.Common.Configurations;
using Trackit.Infra.Persistence;

namespace Trackit.Common.Extensions;

public static class BuilderExtension
{
  public static void AddBuilderConfiguration(this WebApplicationBuilder builder)
  {
    builder.AddSwaggerConfiguration();
    builder.Host.AddSerilogConfiguration();

    builder.Services.AddScoped<TokenService>();
    builder.Services.AddScoped<FileService>();

    builder.Services.AddDbContextFactory<AppDbContext>(config =>
    {
      config.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Services.Configure
    <Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options =>
      {
        options
          .SerializerOptions
          .Converters
          .Add(new JsonStringEnumConverter());
      }
    );

    builder.Services
      .AddCors(x => 
      {
        x.AddPolicy("DevelopmentPolicy", policy => {
          policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
      }
    );
  }
}