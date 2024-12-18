using System.Text.Json.Serialization;

namespace Trackit.Extensions;

public static class BuilderExtensions
{
  public static void AddBuilderExtensions(this WebApplicationBuilder builder)
  {
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
      options
        .JsonSerializerOptions
        .Converters
        .Add(new JsonStringEnumConverter());
    });
  }
}