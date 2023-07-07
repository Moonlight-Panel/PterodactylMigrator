namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Subuser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ServerId { get; set; }
    public string? Permissions { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Server Server { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}