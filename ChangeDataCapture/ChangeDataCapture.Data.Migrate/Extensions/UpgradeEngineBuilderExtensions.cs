using DbUp.Builder;
using DbUp.Engine;
using DbUp.ScriptProviders;
using DbUp.Support;
using ChangeDataCapture.Data.Migrate.Configuration;

namespace ChangeDataCapture.Data.Migrate.Extensions;

internal static class UpgradeEngineBuilderExtensions
{
    public static UpgradeEngineBuilder WithRunOnceScripts(
        this UpgradeEngineBuilder builder,
        IMigrateConfiguration configuration)
    {
        var sqlScriptOptions = new SqlScriptOptions
        {
            ScriptType = ScriptType.RunOnce
        };

        return builder.WithScriptsFromFileSystem(
            configuration.ScriptsPath,
            GetFileSystemScriptOptions(configuration, "1 - RunOnce"),
            sqlScriptOptions);
    }

    public static UpgradeEngineBuilder WithRunAlwaysScripts(
        this UpgradeEngineBuilder builder,
        IMigrateConfiguration configuration)
    {
        var sqlScriptOptions = new SqlScriptOptions
        {
            ScriptType = ScriptType.RunAlways
        };

        return builder.WithScriptsFromFileSystem(
            configuration.ScriptsPath,
            GetFileSystemScriptOptions(configuration, "2 - RunAlways"),
            sqlScriptOptions);
    }

    private static FileSystemScriptOptions GetFileSystemScriptOptions(
        IMigrateConfiguration configuration,
        string pathSuffix)
    {
        return new FileSystemScriptOptions
        {
            IncludeSubDirectories = true,
            Extensions = new[] { "*.sql" },
            Filter = script =>
            {
                return script.StartsWith(Path.Combine(configuration.ScriptsPath, $"{pathSuffix}{Path.DirectorySeparatorChar}"));
            }
        };
    }
}
