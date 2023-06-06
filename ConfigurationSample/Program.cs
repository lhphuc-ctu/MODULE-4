using ConfigurationSample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Configuration.AddIniFile("MyIniConfig.ini", optional: true, reloadOnChange: true);
//builder.Configuration.AddJsonFile("MyConfig.json", optional : true, reloadOnChange: true);
//builder.Configuration.AddXmlFile("XmlConfig.xml", optional:true, reloadOnChange: true);

//builder.Services.Configure<MyInfoOptions>(builder.Configuration.GetSection(MyInfoOptions.MyInfo));

builder.Services.AddOptions<MyInfoOptions>()
    .Bind(builder.Configuration.GetSection(MyInfoOptions.MyInfo))
    .ValidateDataAnnotations();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Console.WriteLine($"My key - {app.Configuration["MyKey"]}");

Console.WriteLine(app.Environment.EnvironmentName);

//foreach (var c in builder.Configuration.AsEnumerable())
//{
//    Console.WriteLine(c.Key + " = " + c.Value);
//}

app.Run();
