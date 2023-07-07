namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Allocation
{
    public int Id { get; set; }
    public int NodeId { get; set; }
    public string Ip { get; set; } = null!;
    public string? IpAlias { get; set; }
    public int Port { get; set; }
    public int? ServerId { get; set; }
    public string? Notes { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Node Node { get; set; } = null!;
    public virtual Server? Server { get; set; }
    public virtual Server ServerNavigation { get; set; } = null!;
}