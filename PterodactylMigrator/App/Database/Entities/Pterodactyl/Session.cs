namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Session
{
    public string Id { get; set; } = null!;
    public int? UserId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string Payload { get; set; } = null!;
    public int LastActivity { get; set; }
}