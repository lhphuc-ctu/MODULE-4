using Microsoft.AspNetCore.OutputCaching;
using OutputCaching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();

var app = builder.Build();

app.UseOutputCache();

app.MapGet("/", Gravatar.WriteGravatar);
app.MapGet("/blog", Gravatar.WriteGravatar)
    .CacheOutput(builder => builder.Tag("tag-blog"));

app.MapPost("/purge/{tag}", async (IOutputCacheStore cache, string tag) =>
{
    await cache.EvictByTagAsync(tag, default);
});
app.MapGet("/etag", async (context) =>
{
    var etag = $"\"{Guid.NewGuid():n}\"";
    context.Response.Headers.ETag = etag;
    await Gravatar.WriteGravatar(context);
}).CacheOutput();

var request = 0;
app.MapGet("/number", async (context) =>
{
    await Task.Delay(2000);
    await context.Response.WriteAsync($"<pre>{request++}</pre>");
}).CacheOutput(builder => builder.SetLocking(true).Expire(TimeSpan.FromSeconds(2)));
app.Run();
