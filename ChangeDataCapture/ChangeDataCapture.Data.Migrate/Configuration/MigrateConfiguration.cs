using Microsoft.Extensions.Configuration;

namespace ChangeDataCapture.Data.Migrate.Configuration;

internal class MigrateConfiguration : IMigrateConfiguration
{
    public string ConnectionString { get; }
    public string ScriptsPath { get; }
    public int RetryCount { get; }
    public TimeSpan RetryDelayDuration { get; }
    public string Environment { get; }

    public MigrateConfiguration(CommandLineOptions options, IConfiguration configuration)
    {
        ConnectionString = options.ConnectionString ?? configuration.GetConnectionString("DefaultConnection");
        ScriptsPath = options.ScriptBasePath ?? Path.Combine(Directory.GetCurrentDirectory(), "Migrations");
        RetryCount = options.RetryCount ?? configuration.GetSection("Migration").GetValue<int>("RetryCount");
        Environment = options.Environment ?? configuration.GetSection("Migration").GetValue<string>("Environment");

        var retryDelayInSeconds = options.RetryDelaySeconds ?? configuration.GetSection("Migration").GetValue<int>("RetryDelaySeconds");
        RetryDelayDuration = TimeSpan.FromSeconds(retryDelayInSeconds);
    }
}
