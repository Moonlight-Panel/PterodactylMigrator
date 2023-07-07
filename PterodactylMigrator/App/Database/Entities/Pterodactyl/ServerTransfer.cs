namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class ServerTransfer
{
    public int Id { get; set; }
    public int ServerId { get; set; }
    public bool? Successful { get; set; }
    public int OldNode { get; set; }
    public int NewNode { get; set; }
    public int OldAllocation { get; set; }
    public int NewAllocation { get; set; }
    public string? OldAdditionalAllocations { get; set; }
    public string? NewAdditionalAllocations { get; set; }
    public bool Archived { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Server Server { get; set; } = null!;
}