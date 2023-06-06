using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using TestMiddleware;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseTiming();
app.MapGet("/", () => "Hello World!").RequireRateLimiting("fixedwindowpolicy");
app.MapGet("/hello", () => "Hello Page!").RequireRateLimiting("tokenbucketpolicy");

app.Run();
