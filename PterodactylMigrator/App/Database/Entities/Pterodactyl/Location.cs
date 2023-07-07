namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Location
{
    public Location()
    {
        Nodes = new HashSet<Node>();
    }

    public int Id { get; set; }
    public string Short { get; set; } = null!;
    public string? Long { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Node> Nodes { get; set; }
}