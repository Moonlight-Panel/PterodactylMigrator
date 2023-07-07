namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class ApiLog
{
    public int Id { get; set; }
    public bool Authorized { get; set; }
    public string? Error { get; set; }
    public string? Key { get; set; }
    public string Method { get; set; } = null!;
    public string Route { get; set; } = null!;
    public string? Content { get; set; }
    public string UserAgent { get; set; } = null!;
    public string RequestIp { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}