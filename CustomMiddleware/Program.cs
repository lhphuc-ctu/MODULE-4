using CustomMiddleware;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMessageWriter, LoggingMessageWriter>();
var app = builder.Build();

app.UseRequestCulture();
app.UseMyCustomMiddleware();

app.Run(async (context) =>
{
    await context.Response.WriteAsync(
        $"CurrentCulture.DisplayName: {CultureInfo.CurrentCulture.DisplayName}");
});

app.Run();