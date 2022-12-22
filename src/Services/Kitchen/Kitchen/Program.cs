using System.Collections;
using Domain.Common.Exceptions;
using Kitchen.DAL;
using Kitchen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kitchen;

internal class Program
{
    protected Program() { }
    private static void Main(string[] args)
    {
        var inEnv = Environment.GetEnvironmentVariable("DOTNET_ENV");
        var env = inEnv != null ? inEnv : "DEV";

        var settingsFile = "appsettings.json";
        if (env != "PRODUCTION")
        {
            settingsFile = $"appsettings.{env.ToLower()}.json";
        }

        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(settingsFile);

        var config = configBuilder.Build();

        Console.WriteLine($"Rabbit Host: {config["RMQ_HOST"]}; Version: {config["DL_INTERNAL_TAG"]}");

        var builder = new ServiceCollection()
            .AddScoped<IKitchenService, KitchenService>()
            .AddSingleton<IConfiguration>(config)
            .AddDbContext<KitchenDbContext>(options =>
            {
                options.UseSqlite($"Data Source={config["DL_INTERNAL_TAG"]}-sql.db");
            })
            .AddSingleton<IKitchenRepository, KitchenRepository>()
            .BuildServiceProvider();

        builder.GetRequiredService<KitchenDbContext>().Database.EnsureCreated();

        var app = new KitchenWorker(builder.GetRequiredService<IKitchenService>(), builder.GetRequiredService<IConfiguration>());

        // Start listening to queue
        app.Run();
    }
}
