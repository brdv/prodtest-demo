using Domain.Common.Exceptions;
using Kitchen.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kitchen;
class Program
{
    static void Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        var versionSetting = Environment.GetEnvironmentVariable("DL_TAG_VERSION");

        if (String.IsNullOrEmpty(environment)) environment = "DEVELOPMENT";
        if (versionSetting == null) throw new EnvironmentVariableException("The environment variable 'DL_TAG_VERSION' was not set.'");

        var RMQHost = environment == "DEVELOPMENT" ? "localhost" : Environment.GetEnvironmentVariable("RMQ_HOST");
        if (String.IsNullOrEmpty(RMQHost)) throw new EnvironmentVariableException("The environment variable 'RMQ_HOST' was not set.");

        Console.WriteLine($"Rabbit Host: {RMQHost}; Environment: {environment}");

        var builder = new ServiceCollection()
            .AddScoped<IKitchenService, KitchenService>()
            .BuildServiceProvider();

        var app = new KitchenWorker(versionSetting, RMQHost, builder.GetRequiredService<IKitchenService>());

        // Start listening to queue
        app.Run();
    }
}