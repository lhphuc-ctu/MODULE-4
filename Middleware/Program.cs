using Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello world!");
//});

app.UseTiming();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from 2nd delegate.");
});

app.Run();
