var builder = WebApplication.CreateBuilder(args);

builder.AddBuilderExtensions();

var app = builder.Build();

app.AddAPPExtensions();