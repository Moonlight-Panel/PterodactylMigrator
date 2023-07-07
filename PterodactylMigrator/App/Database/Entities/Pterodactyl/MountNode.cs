namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class MountNode
{
    public int NodeId { get; set; }
    public int MountId { get; set; }

    public virtual Mount Mount { get; set; } = null!;
    public virtual Node Node { get; set; } = null!;
}