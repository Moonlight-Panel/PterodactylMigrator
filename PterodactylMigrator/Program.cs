using PterodactylMigrator.App;
using PterodactylMigrator.App.Database.Contexts;
using PterodactylMigrator.App.Migrators;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();
    
Log.Information("Starting pterodactyl migrator");

static string ReadMaskedLine()
{
    string input = "";
    ConsoleKeyInfo keyInfo;

    do
    {
        keyInfo = Console.ReadKey(true);

        // Ignore any non-character keys
        if (!char.IsControl(keyInfo.KeyChar))
        {
            input += keyInfo.KeyChar;
            Console.Write("*"); // Mask character
        }
        else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
            Console.Write("\b \b"); // Clear previous character
        }
    } while (keyInfo.Key != ConsoleKey.Enter);

    Console.WriteLine(); // Move to next line

    return input;
}

Log.Information("Enter the data for your pterodactyl database");

Console.Write("Host: ");
MigratorConfig.PterodactylDatabaseConfig.Host = Console.ReadLine() ?? "";

Console.Write("Port: ");
MigratorConfig.PterodactylDatabaseConfig.Port = int.Parse(Console.ReadLine() ?? "3306");

Console.Write("Username: ");
MigratorConfig.PterodactylDatabaseConfig.Username = Console.ReadLine() ?? "";

Console.Write("Password: ");
MigratorConfig.PterodactylDatabaseConfig.Password = ReadMaskedLine();

Console.Write("Database: ");
MigratorConfig.PterodactylDatabaseConfig.Database = Console.ReadLine() ?? "";

Log.Information("Enter the data for your moonlight database");

Console.Write("Host: ");
MigratorConfig.MoonlightDatabaseConfig.Host = Console.ReadLine() ?? "";

Console.Write("Port: ");
MigratorConfig.MoonlightDatabaseConfig.Port = int.Parse(Console.ReadLine() ?? "3306");

Console.Write("Username: ");
MigratorConfig.MoonlightDatabaseConfig.Username = Console.ReadLine() ?? "";

Console.Write("Password: ");
MigratorConfig.MoonlightDatabaseConfig.Password = ReadMaskedLine();

Console.Write("Database: ");
MigratorConfig.MoonlightDatabaseConfig.Database = Console.ReadLine() ?? "";

Log.Information("Enter the pterodactyl app secret");
Console.Write("App key: ");
MigratorConfig.AppKey = ReadMaskedLine();

Log.Information("Done. Press enter to start migration");
Console.ReadLine();

using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
{
    if (ml.Database.EnsureCreated())
    {
        Log.Information("Created database structure");
    }
    else
    {
        Log.Information("Database was already there");
    }
}

new UserMigrator().Migrate();
new NodeMigrator().Migrate();
new ImageMigrator().Migrate();
new ServerMigrator().Migrate();