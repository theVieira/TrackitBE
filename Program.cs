using DotNetEnv;
using Trackit.Common.Extensions;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.AddBuilderConfiguration();

var app = builder.Build();

app.AddAppConfiguration();
