public static class AppExtensions
{
  public static void AddAPPExtensions(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.Run();
  }
}