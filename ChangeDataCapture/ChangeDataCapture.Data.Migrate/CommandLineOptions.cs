using CommandLine;

namespace ChangeDataCapture.Data.Migrate;

public class CommandLineOptions
{
    [Option('c', "ConnectionString", Required = false, HelpText = "Input Connection String.")]
    public string? ConnectionString { get; set; }

    [Option('p', "ScriptBasePath", Required = false, HelpText = "Input base path to the root script folder.")]
    public string? ScriptBasePath { get; set; }

    [Option('r', "RetryCount", Required = false, HelpText = "Migration Retry Count.")]
    public int? RetryCount { get; set; }

    [Option('d', "RetryDelaySeconds", Required = false, HelpText = "Retry Delay Duration in seconds.")]
    public int? RetryDelaySeconds { get; set; }

    [Option('e', "Environment", Required = false, HelpText = "Environment.")]
    public string? Environment { get; set; }
}
