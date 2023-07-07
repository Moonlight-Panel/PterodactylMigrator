namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class AuditLog
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public bool IsSystem { get; set; }
    public int? UserId { get; set; }
    public int? ServerId { get; set; }
    public string Action { get; set; } = null!;
    public string? Subaction { get; set; }
    public string Device { get; set; } = null!;
    public string Metadata { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public virtual Server? Server { get; set; }
    public virtual User? User { get; set; }
}