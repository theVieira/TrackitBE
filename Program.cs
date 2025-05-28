using DotNetEnv;
using Trackit.Common.Extensions;
using Trackit.Infra.Persistence;
using Trackit.Seed;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.AddBuilderConfiguration();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var config = services.GetRequiredService<IConfiguration>();
    
    var defaultUser = new CreateDefaultUser(context, config);
    await defaultUser.Create();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Running API - 🚀");
}

app.AddAppConfiguration();