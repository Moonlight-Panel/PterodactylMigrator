namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class RecoveryToken
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}