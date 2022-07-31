using CommandLine;
using ChangeDataCapture.Data.Migrate.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChangeDataCapture.Data.Migrate;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var container = new ServiceCollection();

        container.AddLogging(builder => builder.AddConsole());
        container.AddSingleton<IConfiguration>(configuration);
        container.AddSingleton<IMigrateConfiguration, MigrateConfiguration>();
        container.AddSingleton<IDBDeployer, PostgresDeployer>();

        Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(options => container.AddSingleton(options));

        container
            .BuildServiceProvider()
            .GetRequiredService<IDBDeployer>()
            .Deploy();
    }
}
