using Microsoft.AspNetCore.RateLimiting;
using RateLimit;
using System.Globalization;
using System.Threading.RateLimiting;

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


builder.Services.AddRateLimiter(option =>
{
    //option.OnRejected = (context, rejectOption) =>
    //{
    //    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
    //    {
    //        context.HttpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
    //    }
    //    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
    //    context.HttpContext.Response.WriteAsync("Too many requests. Please try again later!");

    //    return new ValueTask();
    //};
    //option.GlobalLimiter = PartitionedRateLimiter.CreateChained(
    //    PartitionedRateLimiter.Create<HttpContext, string>(HttpContext =>
    //    {
    //        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

    //        return RateLimitPartition.GetFixedWindowLimiter(userAgent, opt => new FixedWindowRateLimiterOptions
    //        {
    //            AutoReplenishment = true,
    //            PermitLimit = 1,
    //            Window = TimeSpan.FromSeconds(5)
    //        });
    //    }),
    //    PartitionedRateLimiter.Create<HttpContext, string>(HttpContext =>
    //    {
    //        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();
    //        return RateLimitPartition.GetFixedWindowLimiter(userAgent, opt => new FixedWindowRateLimiterOptions
    //        {
    //            AutoReplenishment = true,
    //            PermitLimit = 5,
    //            Window = TimeSpan.FromSeconds(60)
    //        });
    //    }));
    option.AddPolicy<string, SampleRateLimiterPolicy>("customRateLimitPolicy");
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

app.UseRateLimiter();

app.MapControllers();

static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

app.MapGet("/", () => Results.Ok($"Hello {GetTicks()}"));

app.MapGet("/hello", () => "Hello World!").RequireRateLimiting("customRateLimitPolicy");


app.Run();
