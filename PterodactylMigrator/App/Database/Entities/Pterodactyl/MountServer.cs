namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class MountServer
{
    public int ServerId { get; set; }
    public int MountId { get; set; }

    public virtual Mount Mount { get; set; } = null!;
    public virtual Server Server { get; set; } = null!;
}