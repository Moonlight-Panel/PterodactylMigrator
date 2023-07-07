namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Schedule
{
    public Schedule()
    {
        Tasks = new HashSet<Task>();
    }

    public int Id { get; set; }
    public int ServerId { get; set; }
    public string Name { get; set; } = null!;
    public string CronDayOfWeek { get; set; } = null!;
    public string CronMonth { get; set; } = null!;
    public string CronDayOfMonth { get; set; } = null!;
    public string CronHour { get; set; } = null!;
    public string CronMinute { get; set; } = null!;
    public bool IsActive { get; set; }
    public bool IsProcessing { get; set; }
    public byte OnlyWhenOnline { get; set; }
    public DateTime? LastRunAt { get; set; }
    public DateTime? NextRunAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Server Server { get; set; } = null!;
    public virtual ICollection<Task> Tasks { get; set; }
}