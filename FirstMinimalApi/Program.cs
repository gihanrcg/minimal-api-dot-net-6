using FirstMinimalApi.models;
using FirstMinimalApi.routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouterDefinitions(typeof(User));

var app = builder.Build();

app.Use(async (context, next) =>
{
    app.Logger.LogInformation("===========================");
    app.Logger.LogInformation($"Initiated {context.Request.Host.Value}{context.Request.Path.Value} endpoint");
    app.Logger.LogInformation("===========================");
    await next();
});

app.UseRouterDefinitions();

app.Use(async (context, next) =>
{
    app.Logger.LogInformation("===========================");
    app.Logger.LogInformation($"Initiated {context.Request.Host.Value}{context.Request.Path.Value} endpoint");
    app.Logger.LogInformation("===========================");
    await next();
});

app.Run("http://localhost:3000");

