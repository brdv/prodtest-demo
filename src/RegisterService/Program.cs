using ProdtestApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IKitchenService, KitchenService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
