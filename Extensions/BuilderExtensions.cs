public static class BuilderExtensions
{
  public static void AddBuilderExtensions(this WebApplicationBuilder builder)
  {
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
  }
}