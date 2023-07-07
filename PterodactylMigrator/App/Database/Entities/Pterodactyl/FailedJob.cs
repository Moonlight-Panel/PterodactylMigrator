namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class FailedJob
{
    public int Id { get; set; }
    public string Connection { get; set; } = null!;
    public string Queue { get; set; } = null!;
    public string Payload { get; set; } = null!;
    public DateTime FailedAt { get; set; }
    public string Exception { get; set; } = null!;
}