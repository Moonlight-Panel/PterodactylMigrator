namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Backup
{
    public long Id { get; set; }
    public int ServerId { get; set; }
    public Guid Uuid { get; set; }
    public string? UploadId { get; set; }
    public bool IsSuccessful { get; set; }
    public byte IsLocked { get; set; }
    public string Name { get; set; } = null!;
    public string IgnoredFiles { get; set; } = null!;
    public string Disk { get; set; } = null!;
    public string? Checksum { get; set; }
    public long Bytes { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual Server Server { get; set; } = null!;
}