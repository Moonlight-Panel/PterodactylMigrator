using Microsoft.EntityFrameworkCore;
using PterodactylMigrator.App.Database.Entities.Moonlight;
using PterodactylMigrator.App.Database.Entities.Moonlight.LogsEntries;
using PterodactylMigrator.App.Database.Entities.Moonlight.Notification;
using PterodactylMigrator.App.Models;

namespace PterodactylMigrator.App.Database.Contexts;

public class MoonlightContext : DbContext
{
    private readonly DatabaseConfig DatabaseConfig;
    
    public MoonlightContext(DatabaseConfig config)
    {
        DatabaseConfig = config;
    }

    public DbSet<DockerImage> DockerImages { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ImageVariable> ImageVariables { get; set; }
    public DbSet<Node> Nodes { get; set; }
    public DbSet<NodeAllocation> NodeAllocations { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<ServerBackup> ServerBackups { get; set; }
    public DbSet<ServerVariable> ServerVariables { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<LoadingMessage> LoadingMessages { get; set; }
    public DbSet<AuditLogEntry> AuditLog { get; set; }
    public DbSet<ErrorLogEntry> ErrorLog { get; set; }
    public DbSet<SecurityLogEntry> SecurityLog { get; set; }

    public DbSet<SharedDomain> SharedDomains { get; set; }
    public DbSet<Domain> Domains { get; set; }
    public DbSet<Revoke> Revokes { get; set; }
    public DbSet<NotificationClient> NotificationClients { get; set; }
    public DbSet<NotificationAction> NotificationActions { get; set; }
    public DbSet<DdosAttack> DdosAttacks { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<StatisticsData> Statistics { get; set; }
    public DbSet<NewsEntry> NewsEntries { get; set; }
    
    public DbSet<CloudPanel> CloudPanels { get; set; }
    public DbSet<MySqlDatabase> Databases { get; set; }
    public DbSet<WebSpace> WebSpaces { get; set; }
    public DbSet<SupportChatMessage> SupportChatMessages { get; set; }
    public DbSet<IpBan> IpBans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = $"host={DatabaseConfig.Host};" +
                                   $"port={DatabaseConfig.Port};" +
                                   $"database={DatabaseConfig.Database};" +
                                   $"uid={DatabaseConfig.Username};" +
                                   $"pwd={DatabaseConfig.Password}";
            
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.Parse("5.7.37-mysql"),
                builder =>
                {
                    builder.EnableRetryOnFailure(5);
                }
            );
        }
    }
}