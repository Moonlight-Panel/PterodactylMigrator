namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class TasksLog
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public DateTime RunTime { get; set; }
    public int RunStatus { get; set; }
    public string Response { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}