using System.Net.Sockets;
using ChangeDataCapture.Data.Migrate.Configuration;
using ChangeDataCapture.Data.Migrate.Extensions;
using DbUp;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace ChangeDataCapture.Data.Migrate;

internal class PostgresDeployer : IDBDeployer
{
    private readonly ILogger<PostgresDeployer> _logger;
    private readonly IMigrateConfiguration _configuration;

    public PostgresDeployer(IMigrateConfiguration configuration, ILogger<PostgresDeployer> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void Deploy() =>
        Policy
            .Handle<SocketException>()
            .Or<NpgsqlException>()
            .WaitAndRetry(
                _configuration.RetryCount,
                currentAttemptNumber =>
                {
                    _logger.LogError(
                        "Migration failed. Current attempt {Attempt}, retrying in {RetryDelay} seconds",
                        currentAttemptNumber,
                        _configuration.RetryDelayDuration.Seconds);

                    return _configuration.RetryDelayDuration;
                })
            .Execute(() =>
            {
                EnsureDatabase.For
                    .PostgresqlDatabase(_configuration.ConnectionString);

                var result = DeployChanges.To
                    .PostgresqlDatabase(_configuration.ConnectionString)
                    .WithRunOnceScripts(_configuration)
                    .WithRunAlwaysScripts(_configuration)
                    .WithVariablesDisabled()
                    .WithoutTransaction()
                    .Build()
                    .PerformUpgrade();

                if (!result.Successful)
                {
                    throw result.Error;
                }
            });
}
