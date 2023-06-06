using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using TestServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Rate limit
builder.Services.AddRateLimiter(option =>
    option.AddFixedWindowLimiter("fixedwindowpolicy", opt => //Fixed Window Limiter
    {
        //Set thoi gian cua Window la 20s
        opt.Window = TimeSpan.FromSeconds(20);
        //Set so luong request toi da: 5
        opt.PermitLimit = 5;
        //Set so luong toi da trong hang doi: 1
        opt.QueueLimit = 1;
        //Set thu tu lay ra cua hang doi: olderfirst 
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }).RejectionStatusCode = 429 // Too Many Requests
);

builder.Services.AddRateLimiter(option =>
    option.AddSlidingWindowLimiter("slidingwindowpolicy", opt => //Sliding Window Limiter
    {
        //Set thoi gian cua Window la 20s
        opt.Window = TimeSpan.FromSeconds(20);
        //Set so luong request toi da: 5
        opt.PermitLimit = 5;
        //Set so luong toi da trong hang doi: 1
        opt.QueueLimit = 1;
        //Set thu tu lay ra cua hang doi: olderfirst 
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        //So luong Segment cua Window
        opt.SegmentsPerWindow = 4;
    }).RejectionStatusCode = 429 // Too Many Requests
);

builder.Services.AddRateLimiter(option =>
    option.AddTokenBucketLimiter("tokenbucketpolicy", opt => //Token Bucket Limiter
    {
        //So luong token mac dinh
        opt.TokenLimit = 5;
        //So luong toi da trong hang doi
        opt.QueueLimit = 1;
        //Thu tu lay ra cua hang doi
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        //Thoi gian them token moi
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(30);
        //So luong token moi
        opt.TokensPerPeriod = 1;
        //Tu dong them token moi
        opt.AutoReplenishment = true;
    }).RejectionStatusCode = 429
);

builder.Services.AddRateLimiter(option =>
    option.AddConcurrencyLimiter("concurrencypolicy", opt => //Concurrency Limiter
    {
        //So luong request toi da
        opt.PermitLimit = 5;
        //So luong toi da trong hang doi
        opt.QueueLimit = 1;
        //Thu tu lay ra cua hang doi
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }).RejectionStatusCode = 429
);

var app = builder.Build();


app.UseTiming();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
