using FilterSample;
using FilterSample.Filters;
using FilterSample.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LoggingExecutionTimeServiceFilter>();
builder.Services.AddScoped<LoggingExecutionTimeActionFilter>();
builder.Services.AddScoped<LoggingExecutionTimeResourceFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<LoggingExecutionTimeServiceFilter>();
    options.Filters.Add<LoggingExecutionTimeActionFilter>();
    options.Filters.Add<LoggingExecutionTimeResourceFilter>();
    //options.Filters.Add(new MyFilter());
    //options.Filters.Add(new MyAsyncFilter());
   // options.Filters.Add(new MyResourceFilter());
   // options.Filters.AddService<MyServiceFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseTiming();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
