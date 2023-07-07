namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class User
{
    public User()
    {
        ApiKeys = new HashSet<ApiKey>();
        AuditLogs = new HashSet<AuditLog>();
        RecoveryTokens = new HashSet<RecoveryToken>();
        Servers = new HashSet<Server>();
        Subusers = new HashSet<Subuser>();
        UserSshKeys = new HashSet<UserSshKey>();
    }

    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public Guid Uuid { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? NameFirst { get; set; }
    public string? NameLast { get; set; }
    public string Password { get; set; } = null!;
    public string? RememberToken { get; set; }
    public string Language { get; set; } = null!;
    public byte RootAdmin { get; set; }
    public byte UseTotp { get; set; }
    public string? TotpSecret { get; set; }
    public DateTime? TotpAuthenticatedAt { get; set; }
    public bool? Gravatar { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ApiKey> ApiKeys { get; set; }
    public virtual ICollection<AuditLog> AuditLogs { get; set; }
    public virtual ICollection<RecoveryToken> RecoveryTokens { get; set; }
    public virtual ICollection<Server> Servers { get; set; }
    public virtual ICollection<Subuser> Subusers { get; set; }
    public virtual ICollection<UserSshKey> UserSshKeys { get; set; }
}