namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class UserSshKey
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Fingerprint { get; set; } = null!;
    public string PublicKey { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual User User { get; set; } = null!;
}