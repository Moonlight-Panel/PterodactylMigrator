namespace PterodactylMigrator.App.Database.Entities.Moonlight.Notification;

public class NotificationAction
{
    public int Id { get; set; }
    public NotificationClient NotificationClient { get; set; }
    public string Action { get; set; }
}