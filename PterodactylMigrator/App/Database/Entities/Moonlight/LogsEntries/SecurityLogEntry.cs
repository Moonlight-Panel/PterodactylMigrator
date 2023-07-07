using PterodactylMigrator.App.Models;

namespace PterodactylMigrator.App.Database.Entities.Moonlight.LogsEntries;

public class SecurityLogEntry
{
    public int Id { get; set; }
    public bool System { get; set; }
    public string Ip { get; set; } = "";
    public SecurityLogType Type { get; set; }
    public string JsonData { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}