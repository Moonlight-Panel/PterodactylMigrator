namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class EggMount
{
    public int EggId { get; set; }
    public int MountId { get; set; }

    public virtual Egg Egg { get; set; } = null!;
    public virtual Mount Mount { get; set; } = null!;
}