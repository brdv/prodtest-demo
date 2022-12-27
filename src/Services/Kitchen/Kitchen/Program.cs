using System.Collections;
using Domain.Common.Exceptions;
using Kitchen.DAL;
using Kitchen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace Kitchen;

internal class Program
{
    protected Program() { }
    private static void Main(string[] args)
    {
        var inEnv = Environment.GetEnvironmentVariable("PRODTEST_VERSION");
        var env = inEnv != null ? inEnv : "DEV";

        var settingsFile = "appsettings.json";
        if (env != "latest")
        {
            settingsFile = $"appsettings.{env.ToLower()}.json";
        }

        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(settingsFile);

        var config = configBuilder.Build();

        Console.WriteLine($"Connecting to server: {config["DB_URL"]}; user: {config["DB_USER"]}; password: {config["DB_PWD"]}; with version {Environment.GetEnvironmentVariable("PRODTEST_VERSION")}");

        var builder = new ServiceCollection()
            .AddScoped<IKitchenService, KitchenService>()
            .AddSingleton<IConfiguration>(config)
            .AddDbContext<KitchenDbContext>(options =>
            {
                options.UseMySQL($"server={config["DB_URL"]};database=prodtest;user={config["DB_USER"]};password={config["DB_PWD"]}");
            })
            .AddSingleton<IKitchenRepository, KitchenRepository>()
            .BuildServiceProvider();

        builder.GetRequiredService<KitchenDbContext>().Database.EnsureCreated();

        var app = new KitchenWorker(builder.GetRequiredService<IKitchenService>(), builder.GetRequiredService<IConfiguration>());

        // Start listening to queue
        app.Run();
    }
}


// CREATE USER 'prodtest'@'localhost' IDENTIFIED BY 'Prodtest!23';
// GRANT ALL PRIVILEGES ON prodtest.* TO 'prodtest'@'localhost';
