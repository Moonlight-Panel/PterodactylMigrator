namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Database
{
    public int Id { get; set; }
    public int ServerId { get; set; }
    public int DatabaseHostId { get; set; }
    public string Database1 { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Remote { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int? MaxConnections { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual DatabaseHost DatabaseHost { get; set; } = null!;
    public virtual Server Server { get; set; } = null!;
}