namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class ActivityLog
{
    public ActivityLog()
    {
        ActivityLogSubjects = new HashSet<ActivityLogSubject>();
    }

    public long Id { get; set; }
    public Guid? Batch { get; set; }
    public string Event { get; set; } = null!;
    public string Ip { get; set; } = null!;
    public string? Description { get; set; }
    public string? ActorType { get; set; }
    public long? ActorId { get; set; }
    public int? ApiKeyId { get; set; }
    public string Properties { get; set; } = null!;
    public DateTime Timestamp { get; set; }

    public virtual ICollection<ActivityLogSubject> ActivityLogSubjects { get; set; }
}