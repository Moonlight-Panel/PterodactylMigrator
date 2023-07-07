namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Task
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int SequenceId { get; set; }
    public string Action { get; set; } = null!;
    public string Payload { get; set; } = null!;
    public int TimeOffset { get; set; }
    public bool IsQueued { get; set; }
    public byte ContinueOnFailure { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;
}