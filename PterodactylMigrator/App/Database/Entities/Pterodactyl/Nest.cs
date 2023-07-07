namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Nest
{
    public Nest()
    {
        Eggs = new HashSet<Egg>();
        Servers = new HashSet<Server>();
    }

    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string Author { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Egg> Eggs { get; set; }
    public virtual ICollection<Server> Servers { get; set; }
}