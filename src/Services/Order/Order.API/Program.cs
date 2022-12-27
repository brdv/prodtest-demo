using Domain.Common.Services;
using Order.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IOrderService, OrderService>()
                .AddScoped<IRabbitMQService, RabbitMQService>();
builder.Services.AddControllers();

var prodtestEnv = Environment.GetEnvironmentVariable("PRODTEST_VERSION");
var env = prodtestEnv != null ? prodtestEnv : "development";

builder.Configuration.AddJsonFile(env == "latest" ? "appsettings.json" : $"appsettings.{env}.json");

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
