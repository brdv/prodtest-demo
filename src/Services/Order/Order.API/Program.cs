

// Stryker disable all : Program.cs not tested
using Domain.Common.Services;
using Order.API.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IOrderService, OrderService>()
                .AddScoped<IRabbitMQService, RabbitMQService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


var prodtestEnv = Environment.GetEnvironmentVariable("PRODTEST_VERSION");
var env = prodtestEnv != null ? prodtestEnv : "development";

builder.Configuration.AddJsonFile(env == "latest" ? "appsettings.json" : $"appsettings.{env}.json");


var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);


app.UseHttpsRedirection();
app.MapControllers();
app.Run();
