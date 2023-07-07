namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class DatabaseHost
{
    public DatabaseHost()
    {
        Databases = new HashSet<Database>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int? MaxDatabases { get; set; }
    public int? NodeId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Node? Node { get; set; }
    public virtual ICollection<Database> Databases { get; set; }
}