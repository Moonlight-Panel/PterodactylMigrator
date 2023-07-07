namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class ApiKey
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public byte KeyType { get; set; }
    public string? Identifier { get; set; }
    public string Token { get; set; } = null!;
    public string? AllowedIps { get; set; }
    public string? Memo { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public byte RServers { get; set; }
    public byte RNodes { get; set; }
    public byte RAllocations { get; set; }
    public byte RUsers { get; set; }
    public byte RLocations { get; set; }
    public byte RNests { get; set; }
    public byte REggs { get; set; }
    public byte RDatabaseHosts { get; set; }
    public byte RServerDatabases { get; set; }

    public virtual User User { get; set; } = null!;
}