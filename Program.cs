using Trackit.Extensions;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.AddBuilderExtensions();

var app = builder.Build();

app.AddAPPExtensions();