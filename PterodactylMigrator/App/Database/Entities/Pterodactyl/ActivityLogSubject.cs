namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class ActivityLogSubject
{
    public long Id { get; set; }
    public long ActivityLogId { get; set; }
    public string SubjectType { get; set; } = null!;
    public long SubjectId { get; set; }

    public virtual ActivityLog ActivityLog { get; set; } = null!;
}