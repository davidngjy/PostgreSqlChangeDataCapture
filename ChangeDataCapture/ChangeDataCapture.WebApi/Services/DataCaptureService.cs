using System.Text.Json;
using Npgsql.Replication;
using Npgsql.Replication.PgOutput;

namespace ChangeDataCapture.WebApi.Services;

public class DataCaptureService : BackgroundService
{
    private const string PublicationName = "example_publication";
    private const string SlotName = "example_slot";

    private readonly string _connectionString;
    private readonly ILogger<DataCaptureService> _logger;

    public DataCaptureService(IConfiguration configuration, ILogger<DataCaptureService> logger)
    {
        _logger = logger;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var connection = new LogicalReplicationConnection(_connectionString);
        await connection.Open(stoppingToken);

        var slot = new PgOutputReplicationSlot(SlotName);
        var publicationOptions = new PgOutputReplicationOptions(PublicationName, 2);

        await foreach (var replicationMessage in connection.StartReplication(slot, publicationOptions, stoppingToken))
        {
            _logger.LogInformation(JsonSerializer.Serialize(replicationMessage));
        }
    }
}
