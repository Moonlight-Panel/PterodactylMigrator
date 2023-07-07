using PterodactylMigrator.App.Models;

namespace PterodactylMigrator.App;

public class MigratorConfig
{
    public static DatabaseConfig MoonlightDatabaseConfig { get; set; } = new();
    public static DatabaseConfig PterodactylDatabaseConfig { get; set; } = new();
    public static string AppKey { get; set; } = "";
}