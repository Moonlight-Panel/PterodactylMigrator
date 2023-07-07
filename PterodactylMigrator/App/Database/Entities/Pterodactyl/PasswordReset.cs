namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class PasswordReset
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}