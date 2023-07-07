namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Mount
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Source { get; set; } = null!;
    public string Target { get; set; } = null!;
    public byte ReadOnly { get; set; }
    public byte UserMountable { get; set; }
}