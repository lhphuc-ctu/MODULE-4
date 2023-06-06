using OutputCahing;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOutputCache(options =>
{
    ////Cai dat cau hinh cho tat ca endpoint
    //options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(10)));
    ////Them policy moi de danh rieng cho mot so endpoint
    //options.AddPolicy("Expire20", builder => builder.Expire(TimeSpan.FromSeconds(20)));
    ////Them policy moi de danh rieng cho mot so endpoint
    //options.AddPolicy("Expire30",builder => builder.Expire(TimeSpan.FromSeconds(30)));

    options.AddBasePolicy(builder => 
        builder.With(c=> c.HttpContext.Request.Path.StartsWithSegments("/blog"))
        .Tag("tag-blog"));
    options.AddBasePolicy(builder => builder.Tag("tag-all"));
    options.AddPolicy("Query", builder => builder.SetVaryByQuery("culture"));
    options.AddPolicy("NoCache", builder => builder.NoCache());
    options.AddPolicy("Nolock",builder => builder.SetLocking(false));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();

// Use in Minimal API
//app.MapGet("/expire10", () =>
//{ return String.Format("Hello world! Time: {0}.", DateTime.Now); }).CacheOutput();

//app.MapGet("/expire30", () =>
//{ return String.Format("Time: {0}.", DateTime.Now); }).CacheOutput("Expire30");

app.MapGet("/", (context) => Gravatar.WriteGravatar(context));

app.Run();
