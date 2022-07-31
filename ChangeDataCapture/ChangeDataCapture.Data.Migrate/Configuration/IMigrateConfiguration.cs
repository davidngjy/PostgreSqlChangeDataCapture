namespace ChangeDataCapture.Data.Migrate.Configuration;

internal interface IMigrateConfiguration
{
    public string ConnectionString { get; }
    public string ScriptsPath { get; }
    public int RetryCount { get; }
    public TimeSpan RetryDelayDuration { get; }
    public string Environment { get; }
}
