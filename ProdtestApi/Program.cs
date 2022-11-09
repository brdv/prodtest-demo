using ProdtestApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IKitchenService, KitchenService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
