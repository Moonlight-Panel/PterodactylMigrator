using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PterodactylMigrator.App.Database.Entities.Pterodactyl;
using PterodactylMigrator.App.Models;

namespace PterodactylMigrator.App.Database.Contexts;

public class PterodactylContext : DbContext
{
    private readonly DatabaseConfig DatabaseConfig;
    
    public PterodactylContext(DatabaseConfig config)
    {
        DatabaseConfig = config;
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
    public virtual DbSet<ActivityLogSubject> ActivityLogSubjects { get; set; } = null!;
    public virtual DbSet<Allocation> Allocations { get; set; } = null!;
    public virtual DbSet<ApiKey> ApiKeys { get; set; } = null!;
    public virtual DbSet<ApiLog> ApiLogs { get; set; } = null!;
    public virtual DbSet<AuditLog> AuditLogs { get; set; } = null!;
    public virtual DbSet<Backup> Backups { get; set; } = null!;
    public virtual DbSet<Egg> Eggs { get; set; } = null!;
    public virtual DbSet<EggMount> EggMounts { get; set; } = null!;
    public virtual DbSet<EggVariable> EggVariables { get; set; } = null!;
    public virtual DbSet<FailedJob> FailedJobs { get; set; } = null!;
    public virtual DbSet<Job> Jobs { get; set; } = null!;
    public virtual DbSet<Location> Locations { get; set; } = null!;
    public virtual DbSet<Migration> Migrations { get; set; } = null!;
    public virtual DbSet<Mount> Mounts { get; set; } = null!;
    public virtual DbSet<MountNode> MountNodes { get; set; } = null!;
    public virtual DbSet<MountServer> MountServers { get; set; } = null!;
    public virtual DbSet<Nest> Nests { get; set; } = null!;
    public virtual DbSet<Node> Nodes { get; set; } = null!;
    public virtual DbSet<Notification> Notifications { get; set; } = null!;
    public virtual DbSet<PasswordReset> PasswordResets { get; set; } = null!;
    public virtual DbSet<RecoveryToken> RecoveryTokens { get; set; } = null!;
    public virtual DbSet<Schedule> Schedules { get; set; } = null!;
    public virtual DbSet<Server> Servers { get; set; } = null!;
    public virtual DbSet<ServerTransfer> ServerTransfers { get; set; } = null!;
    public virtual DbSet<ServerVariable> ServerVariables { get; set; } = null!;
    public virtual DbSet<Session> Sessions { get; set; } = null!;
    public virtual DbSet<Setting> Settings { get; set; } = null!;
    public virtual DbSet<Subuser> Subusers { get; set; } = null!;
    public virtual DbSet<TasksLog> TasksLogs { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UserSshKey> UserSshKeys { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                $"host={DatabaseConfig.Host};" +
                $"port={DatabaseConfig.Port};" +
                $"database={DatabaseConfig.Database};" +
                $"uid={DatabaseConfig.Username};" +
                $"pwd={DatabaseConfig.Password}",
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.37-mysql")
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.ToTable("activity_logs");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.ActorType, e.ActorId }, "activity_logs_actor_type_actor_id_index");

            entity.HasIndex(e => e.Event, "activity_logs_event_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.ActorId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("actor_id");

            entity.Property(e => e.ActorType)
                .HasMaxLength(191)
                .HasColumnName("actor_type");

            entity.Property(e => e.ApiKeyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("api_key_id");

            entity.Property(e => e.Batch).HasColumnName("batch");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Event)
                .HasMaxLength(191)
                .HasColumnName("event");

            entity.Property(e => e.Ip)
                .HasMaxLength(191)
                .HasColumnName("ip");

            entity.Property(e => e.Properties)
                .HasColumnType("json")
                .HasColumnName("properties");

            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp")
                .HasColumnName("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<ActivityLogSubject>(entity =>
        {
            entity.ToTable("activity_log_subjects");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ActivityLogId, "activity_log_subjects_activity_log_id_foreign");

            entity.HasIndex(e => new { e.SubjectType, e.SubjectId },
                "activity_log_subjects_subject_type_subject_id_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.ActivityLogId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("activity_log_id");

            entity.Property(e => e.SubjectId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("subject_id");

            entity.Property(e => e.SubjectType)
                .HasMaxLength(191)
                .HasColumnName("subject_type");

            entity.HasOne(d => d.ActivityLog)
                .WithMany(p => p.ActivityLogSubjects)
                .HasForeignKey(d => d.ActivityLogId)
                .HasConstraintName("activity_log_subjects_activity_log_id_foreign");
        });

        modelBuilder.Entity<Allocation>(entity =>
        {
            entity.ToTable("allocations");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.NodeId, e.Ip, e.Port }, "allocations_node_id_ip_port_unique")
                .IsUnique();

            entity.HasIndex(e => e.ServerId, "allocations_server_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Ip)
                .HasMaxLength(191)
                .HasColumnName("ip");

            entity.Property(e => e.IpAlias)
                .HasColumnType("text")
                .HasColumnName("ip_alias");

            entity.Property(e => e.NodeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("node_id");

            entity.Property(e => e.Notes)
                .HasMaxLength(191)
                .HasColumnName("notes");

            entity.Property(e => e.Port)
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("port");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Node)
                .WithMany(p => p.Allocations)
                .HasForeignKey(d => d.NodeId)
                .HasConstraintName("allocations_node_id_foreign");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.Allocations)
                .HasForeignKey(d => d.ServerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("allocations_server_id_foreign");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.ToTable("api_keys");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Identifier, "api_keys_identifier_unique")
                .IsUnique();

            entity.HasIndex(e => e.UserId, "api_keys_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.AllowedIps)
                .HasColumnType("text")
                .HasColumnName("allowed_ips");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Identifier)
                .HasMaxLength(16)
                .HasColumnName("identifier")
                .IsFixedLength();

            entity.Property(e => e.KeyType)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("key_type");

            entity.Property(e => e.LastUsedAt)
                .HasColumnType("timestamp")
                .HasColumnName("last_used_at");

            entity.Property(e => e.Memo)
                .HasColumnType("text")
                .HasColumnName("memo");

            entity.Property(e => e.RAllocations)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_allocations");

            entity.Property(e => e.RDatabaseHosts)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_database_hosts");

            entity.Property(e => e.REggs)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_eggs");

            entity.Property(e => e.RLocations)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_locations");

            entity.Property(e => e.RNests)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_nests");

            entity.Property(e => e.RNodes)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_nodes");

            entity.Property(e => e.RServerDatabases)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_server_databases");

            entity.Property(e => e.RServers)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_servers");

            entity.Property(e => e.RUsers)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("r_users");

            entity.Property(e => e.Token)
                .HasColumnType("text")
                .HasColumnName("token");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("api_keys_user_id_foreign");
        });

        modelBuilder.Entity<ApiLog>(entity =>
        {
            entity.ToTable("api_logs");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Authorized).HasColumnName("authorized");

            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Error)
                .HasColumnType("text")
                .HasColumnName("error");

            entity.Property(e => e.Key)
                .HasMaxLength(16)
                .HasColumnName("key")
                .IsFixedLength();

            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method")
                .IsFixedLength();

            entity.Property(e => e.RequestIp)
                .HasMaxLength(45)
                .HasColumnName("request_ip");

            entity.Property(e => e.Route)
                .HasColumnType("text")
                .HasColumnName("route");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("audit_logs");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.Action, e.ServerId }, "audit_logs_action_server_id_index");

            entity.HasIndex(e => e.ServerId, "audit_logs_server_id_foreign");

            entity.HasIndex(e => e.UserId, "audit_logs_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Action)
                .HasMaxLength(191)
                .HasColumnName("action");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Device)
                .HasColumnType("json")
                .HasColumnName("device");

            entity.Property(e => e.IsSystem).HasColumnName("is_system");

            entity.Property(e => e.Metadata)
                .HasColumnType("json")
                .HasColumnName("metadata");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.Subaction)
                .HasMaxLength(191)
                .HasColumnName("subaction");

            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ServerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("audit_logs_server_id_foreign");

            entity.HasOne(d => d.User)
                .WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("audit_logs_user_id_foreign");
        });

        modelBuilder.Entity<Backup>(entity =>
        {
            entity.ToTable("backups");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ServerId, "backups_server_id_foreign");

            entity.HasIndex(e => e.Uuid, "backups_uuid_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Bytes)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("bytes");

            entity.Property(e => e.Checksum)
                .HasMaxLength(191)
                .HasColumnName("checksum");

            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("completed_at");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");

            entity.Property(e => e.Disk)
                .HasMaxLength(191)
                .HasColumnName("disk");

            entity.Property(e => e.IgnoredFiles)
                .HasColumnType("text")
                .HasColumnName("ignored_files");

            entity.Property(e => e.IsLocked)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("is_locked");

            entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UploadId)
                .HasColumnType("text")
                .HasColumnName("upload_id");

            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.Backups)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("backups_server_id_foreign");
        });

        modelBuilder.Entity<PterodactylMigrator.App.Database.Entities.Pterodactyl.Database>(entity =>
        {
            entity.ToTable("databases");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.DatabaseHostId, e.ServerId, e.Database1 },
                    "databases_database_host_id_server_id_database_unique")
                .IsUnique();

            entity.HasIndex(e => new { e.DatabaseHostId, e.Username }, "databases_database_host_id_username_unique")
                .IsUnique();

            entity.HasIndex(e => e.ServerId, "databases_server_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Database1)
                .HasMaxLength(191)
                .HasColumnName("database");

            entity.Property(e => e.DatabaseHostId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("database_host_id");

            entity.Property(e => e.MaxConnections)
                .HasColumnType("int(11)")
                .HasColumnName("max_connections")
                .HasDefaultValueSql("'0'");

            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");

            entity.Property(e => e.Remote)
                .HasMaxLength(191)
                .HasColumnName("remote")
                .HasDefaultValueSql("'%'");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.Username)
                .HasMaxLength(191)
                .HasColumnName("username");

            entity.HasOne(d => d.DatabaseHost)
                .WithMany(p => p.Databases)
                .HasForeignKey(d => d.DatabaseHostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("databases_database_host_id_foreign");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.Databases)
                .HasForeignKey(d => d.ServerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("databases_server_id_foreign");
        });

        modelBuilder.Entity<DatabaseHost>(entity =>
        {
            entity.ToTable("database_hosts");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.NodeId, "database_hosts_node_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Host)
                .HasMaxLength(191)
                .HasColumnName("host");

            entity.Property(e => e.MaxDatabases)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("max_databases");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.NodeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("node_id");

            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");

            entity.Property(e => e.Port)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("port");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.Username)
                .HasMaxLength(191)
                .HasColumnName("username");

            entity.HasOne(d => d.Node)
                .WithMany(p => p.DatabaseHosts)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("database_hosts_node_id_foreign");
        });

        modelBuilder.Entity<Egg>(entity =>
        {
            entity.ToTable("eggs");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ConfigFrom, "eggs_config_from_foreign");

            entity.HasIndex(e => e.CopyScriptFrom, "eggs_copy_script_from_foreign");

            entity.HasIndex(e => e.NestId, "service_options_nest_id_foreign");

            entity.HasIndex(e => e.Uuid, "service_options_uuid_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Author)
                .HasMaxLength(191)
                .HasColumnName("author");

            entity.Property(e => e.ConfigFiles)
                .HasColumnType("text")
                .HasColumnName("config_files");

            entity.Property(e => e.ConfigFrom)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("config_from");

            entity.Property(e => e.ConfigLogs)
                .HasColumnType("text")
                .HasColumnName("config_logs");

            entity.Property(e => e.ConfigStartup)
                .HasColumnType("text")
                .HasColumnName("config_startup");

            entity.Property(e => e.ConfigStop)
                .HasMaxLength(191)
                .HasColumnName("config_stop");

            entity.Property(e => e.CopyScriptFrom)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("copy_script_from");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.DockerImages)
                .HasColumnType("json")
                .HasColumnName("docker_images");

            entity.Property(e => e.Features)
                .HasColumnType("json")
                .HasColumnName("features");

            entity.Property(e => e.FileDenylist)
                .HasColumnType("json")
                .HasColumnName("file_denylist");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.NestId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("nest_id");

            entity.Property(e => e.ScriptContainer)
                .HasMaxLength(191)
                .HasColumnName("script_container")
                .HasDefaultValueSql("'alpine:3.4'");

            entity.Property(e => e.ScriptEntry)
                .HasMaxLength(191)
                .HasColumnName("script_entry")
                .HasDefaultValueSql("'ash'");

            entity.Property(e => e.ScriptInstall)
                .HasColumnType("text")
                .HasColumnName("script_install");

            entity.Property(e => e.ScriptIsPrivileged)
                .IsRequired()
                .HasColumnName("script_is_privileged")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.Startup)
                .HasColumnType("text")
                .HasColumnName("startup");

            entity.Property(e => e.UpdateUrl)
                .HasColumnType("text")
                .HasColumnName("update_url");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.ConfigFromNavigation)
                .WithMany(p => p.InverseConfigFromNavigation)
                .HasForeignKey(d => d.ConfigFrom)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("eggs_config_from_foreign");

            entity.HasOne(d => d.CopyScriptFromNavigation)
                .WithMany(p => p.InverseCopyScriptFromNavigation)
                .HasForeignKey(d => d.CopyScriptFrom)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("eggs_copy_script_from_foreign");

            entity.HasOne(d => d.Nest)
                .WithMany(p => p.Eggs)
                .HasForeignKey(d => d.NestId)
                .HasConstraintName("service_options_nest_id_foreign");
        });

        modelBuilder.Entity<EggMount>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("egg_mount");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.EggId, e.MountId }, "egg_mount_egg_id_mount_id_unique")
                .IsUnique();

            entity.HasIndex(e => e.MountId, "egg_mount_mount_id_foreign");

            entity.Property(e => e.EggId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("egg_id");

            entity.Property(e => e.MountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("mount_id");

            entity.HasOne(d => d.Egg)
                .WithMany()
                .HasForeignKey(d => d.EggId)
                .HasConstraintName("egg_mount_egg_id_foreign");

            entity.HasOne(d => d.Mount)
                .WithMany()
                .HasForeignKey(d => d.MountId)
                .HasConstraintName("egg_mount_mount_id_foreign");
        });

        modelBuilder.Entity<EggVariable>(entity =>
        {
            entity.ToTable("egg_variables");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.EggId, "service_variables_egg_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.DefaultValue)
                .HasColumnType("text")
                .HasColumnName("default_value");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.EggId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("egg_id");

            entity.Property(e => e.EnvVariable)
                .HasMaxLength(191)
                .HasColumnName("env_variable");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.Rules)
                .HasColumnType("text")
                .HasColumnName("rules");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UserEditable)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("user_editable");

            entity.Property(e => e.UserViewable)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("user_viewable");

            entity.HasOne(d => d.Egg)
                .WithMany(p => p.EggVariables)
                .HasForeignKey(d => d.EggId)
                .HasConstraintName("service_variables_egg_id_foreign");
        });

        modelBuilder.Entity<FailedJob>(entity =>
        {
            entity.ToTable("failed_jobs");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Connection)
                .HasColumnType("text")
                .HasColumnName("connection");

            entity.Property(e => e.Exception)
                .HasColumnType("text")
                .HasColumnName("exception");

            entity.Property(e => e.FailedAt)
                .HasColumnType("timestamp")
                .HasColumnName("failed_at");

            entity.Property(e => e.Payload).HasColumnName("payload");

            entity.Property(e => e.Queue)
                .HasColumnType("text")
                .HasColumnName("queue");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.ToTable("jobs");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.Queue, e.ReservedAt }, "jobs_queue_reserved_at_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Attempts)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("attempts");

            entity.Property(e => e.AvailableAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("available_at");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("created_at");

            entity.Property(e => e.Payload).HasColumnName("payload");

            entity.Property(e => e.Queue)
                .HasMaxLength(191)
                .HasColumnName("queue");

            entity.Property(e => e.ReservedAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("reserved_at");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("locations");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Short, "locations_short_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Long)
                .HasColumnType("text")
                .HasColumnName("long");

            entity.Property(e => e.Short)
                .HasMaxLength(191)
                .HasColumnName("short");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.ToTable("migrations");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Batch)
                .HasColumnType("int(11)")
                .HasColumnName("batch");

            entity.Property(e => e.Migration1)
                .HasMaxLength(191)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<Mount>(entity =>
        {
            entity.ToTable("mounts");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Id, "mounts_id_unique")
                .IsUnique();

            entity.HasIndex(e => e.Name, "mounts_name_unique")
                .IsUnique();

            entity.HasIndex(e => e.Uuid, "mounts_uuid_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.ReadOnly)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("read_only");

            entity.Property(e => e.Source)
                .HasMaxLength(191)
                .HasColumnName("source");

            entity.Property(e => e.Target)
                .HasMaxLength(191)
                .HasColumnName("target");

            entity.Property(e => e.UserMountable)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("user_mountable");

            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<MountNode>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("mount_node");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.MountId, "mount_node_mount_id_foreign");

            entity.HasIndex(e => new { e.NodeId, e.MountId }, "mount_node_node_id_mount_id_unique")
                .IsUnique();

            entity.Property(e => e.MountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("mount_id");

            entity.Property(e => e.NodeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("node_id");

            entity.HasOne(d => d.Mount)
                .WithMany()
                .HasForeignKey(d => d.MountId)
                .HasConstraintName("mount_node_mount_id_foreign");

            entity.HasOne(d => d.Node)
                .WithMany()
                .HasForeignKey(d => d.NodeId)
                .HasConstraintName("mount_node_node_id_foreign");
        });

        modelBuilder.Entity<MountServer>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("mount_server");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.MountId, "mount_server_mount_id_foreign");

            entity.HasIndex(e => new { e.ServerId, e.MountId }, "mount_server_server_id_mount_id_unique")
                .IsUnique();

            entity.Property(e => e.MountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("mount_id");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.HasOne(d => d.Mount)
                .WithMany()
                .HasForeignKey(d => d.MountId)
                .HasConstraintName("mount_server_mount_id_foreign");

            entity.HasOne(d => d.Server)
                .WithMany()
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("mount_server_server_id_foreign");
        });

        modelBuilder.Entity<Nest>(entity =>
        {
            entity.ToTable("nests");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Uuid, "services_uuid_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Author)
                .HasMaxLength(191)
                .HasColumnName("author")
                .IsFixedLength();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<Node>(entity =>
        {
            entity.ToTable("nodes");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.DaemonTokenId, "nodes_daemon_token_id_unique")
                .IsUnique();

            entity.HasIndex(e => e.LocationId, "nodes_location_id_foreign");

            entity.HasIndex(e => e.Uuid, "nodes_uuid_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.BehindProxy).HasColumnName("behind_proxy");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.DaemonBase)
                .HasMaxLength(191)
                .HasColumnName("daemonBase")
                .HasDefaultValueSql("'/home/daemon-files'");

            entity.Property(e => e.DaemonListen)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("daemonListen")
                .HasDefaultValueSql("'8080'");

            entity.Property(e => e.DaemonSftp)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("daemonSFTP")
                .HasDefaultValueSql("'2022'");

            entity.Property(e => e.DaemonToken)
                .HasColumnType("text")
                .HasColumnName("daemon_token");

            entity.Property(e => e.DaemonTokenId)
                .HasMaxLength(16)
                .HasColumnName("daemon_token_id")
                .IsFixedLength();

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Disk)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("disk");

            entity.Property(e => e.DiskOverallocate)
                .HasColumnType("int(11)")
                .HasColumnName("disk_overallocate");

            entity.Property(e => e.Fqdn)
                .HasMaxLength(191)
                .HasColumnName("fqdn");

            entity.Property(e => e.LocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_id");

            entity.Property(e => e.MaintenanceMode).HasColumnName("maintenance_mode");

            entity.Property(e => e.Memory)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("memory");

            entity.Property(e => e.MemoryOverallocate)
                .HasColumnType("int(11)")
                .HasColumnName("memory_overallocate");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.Public)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("public");

            entity.Property(e => e.Scheme)
                .HasMaxLength(191)
                .HasColumnName("scheme")
                .HasDefaultValueSql("'https'");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UploadSize)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("upload_size")
                .HasDefaultValueSql("'100'");

            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.Nodes)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nodes_location_id_foreign");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.NotifiableType, e.NotifiableId },
                "notifications_notifiable_type_notifiable_id_index");

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");

            entity.Property(e => e.NotifiableId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("notifiable_id");

            entity.Property(e => e.NotifiableType)
                .HasMaxLength(191)
                .HasColumnName("notifiable_type");

            entity.Property(e => e.ReadAt)
                .HasColumnType("timestamp")
                .HasColumnName("read_at");

            entity.Property(e => e.Type)
                .HasMaxLength(191)
                .HasColumnName("type");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("password_resets");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Email, "password_resets_email_index");

            entity.HasIndex(e => e.Token, "password_resets_token_index");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Email)
                .HasMaxLength(191)
                .HasColumnName("email");

            entity.Property(e => e.Token)
                .HasMaxLength(191)
                .HasColumnName("token");
        });

        modelBuilder.Entity<RecoveryToken>(entity =>
        {
            entity.ToTable("recovery_tokens");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "recovery_tokens_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Token)
                .HasMaxLength(191)
                .HasColumnName("token");

            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.RecoveryTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("recovery_tokens_user_id_foreign");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("schedules");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ServerId, "schedules_server_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.CronDayOfMonth)
                .HasMaxLength(191)
                .HasColumnName("cron_day_of_month");

            entity.Property(e => e.CronDayOfWeek)
                .HasMaxLength(191)
                .HasColumnName("cron_day_of_week");

            entity.Property(e => e.CronHour)
                .HasMaxLength(191)
                .HasColumnName("cron_hour");

            entity.Property(e => e.CronMinute)
                .HasMaxLength(191)
                .HasColumnName("cron_minute");

            entity.Property(e => e.CronMonth)
                .HasMaxLength(191)
                .HasColumnName("cron_month");

            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.Property(e => e.IsProcessing).HasColumnName("is_processing");

            entity.Property(e => e.LastRunAt)
                .HasColumnType("timestamp")
                .HasColumnName("last_run_at");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.NextRunAt)
                .HasColumnType("timestamp")
                .HasColumnName("next_run_at");

            entity.Property(e => e.OnlyWhenOnline)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("only_when_online");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("schedules_server_id_foreign");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.ToTable("servers");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AllocationId, "servers_allocation_id_unique")
                .IsUnique();

            entity.HasIndex(e => e.EggId, "servers_egg_id_foreign");

            entity.HasIndex(e => e.ExternalId, "servers_external_id_unique")
                .IsUnique();

            entity.HasIndex(e => e.NestId, "servers_nest_id_foreign");

            entity.HasIndex(e => e.NodeId, "servers_node_id_foreign");

            entity.HasIndex(e => e.OwnerId, "servers_owner_id_foreign");

            entity.HasIndex(e => e.Uuid, "servers_uuid_unique")
                .IsUnique();

            entity.HasIndex(e => e.UuidShort, "servers_uuidshort_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.AllocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("allocation_id");

            entity.Property(e => e.AllocationLimit)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("allocation_limit");

            entity.Property(e => e.BackupLimit)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("backup_limit");

            entity.Property(e => e.Cpu)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("cpu");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.DatabaseLimit)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("database_limit")
                .HasDefaultValueSql("'0'");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Disk)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("disk");

            entity.Property(e => e.EggId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("egg_id");

            entity.Property(e => e.ExternalId)
                .HasMaxLength(191)
                .HasColumnName("external_id");

            entity.Property(e => e.Image)
                .HasMaxLength(191)
                .HasColumnName("image");

            entity.Property(e => e.Io)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("io");

            entity.Property(e => e.Memory)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("memory");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.NestId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("nest_id");

            entity.Property(e => e.NodeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("node_id");

            entity.Property(e => e.OomDisabled)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("oom_disabled");

            entity.Property(e => e.OwnerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("owner_id");

            entity.Property(e => e.SkipScripts).HasColumnName("skip_scripts");

            entity.Property(e => e.Startup)
                .HasColumnType("text")
                .HasColumnName("startup");

            entity.Property(e => e.Status)
                .HasMaxLength(191)
                .HasColumnName("status");

            entity.Property(e => e.Swap)
                .HasColumnType("int(11)")
                .HasColumnName("swap");

            entity.Property(e => e.Threads)
                .HasMaxLength(191)
                .HasColumnName("threads");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.Property(e => e.UuidShort)
                .HasMaxLength(8)
                .HasColumnName("uuidShort")
                .IsFixedLength();

            entity.HasOne(d => d.Allocation)
                .WithOne(p => p.ServerNavigation)
                .HasForeignKey<Server>(d => d.AllocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servers_allocation_id_foreign");

            entity.HasOne(d => d.Egg)
                .WithMany(p => p.Servers)
                .HasForeignKey(d => d.EggId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servers_egg_id_foreign");

            entity.HasOne(d => d.Nest)
                .WithMany(p => p.Servers)
                .HasForeignKey(d => d.NestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servers_nest_id_foreign");

            entity.HasOne(d => d.Node)
                .WithMany(p => p.Servers)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servers_node_id_foreign");

            entity.HasOne(d => d.Owner)
                .WithMany(p => p.Servers)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servers_owner_id_foreign");
        });

        modelBuilder.Entity<ServerTransfer>(entity =>
        {
            entity.ToTable("server_transfers");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ServerId, "server_transfers_server_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Archived).HasColumnName("archived");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.NewAdditionalAllocations)
                .HasColumnType("json")
                .HasColumnName("new_additional_allocations");

            entity.Property(e => e.NewAllocation)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("new_allocation");

            entity.Property(e => e.NewNode)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("new_node");

            entity.Property(e => e.OldAdditionalAllocations)
                .HasColumnType("json")
                .HasColumnName("old_additional_allocations");

            entity.Property(e => e.OldAllocation)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("old_allocation");

            entity.Property(e => e.OldNode)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("old_node");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.Successful).HasColumnName("successful");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.ServerTransfers)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("server_transfers_server_id_foreign");
        });

        modelBuilder.Entity<ServerVariable>(entity =>
        {
            entity.ToTable("server_variables");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ServerId, "server_variables_server_id_foreign");

            entity.HasIndex(e => e.VariableId, "server_variables_variable_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.VariableId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("variable_id");

            entity.Property(e => e.VariableValue)
                .HasColumnType("text")
                .HasColumnName("variable_value");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.ServerVariables)
                .HasForeignKey(d => d.ServerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("server_variables_server_id_foreign");

            entity.HasOne(d => d.Variable)
                .WithMany(p => p.ServerVariables)
                .HasForeignKey(d => d.VariableId)
                .HasConstraintName("server_variables_variable_id_foreign");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("sessions");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Id, "sessions_id_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");

            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");

            entity.Property(e => e.LastActivity)
                .HasColumnType("int(11)")
                .HasColumnName("last_activity");

            entity.Property(e => e.Payload)
                .HasColumnType("text")
                .HasColumnName("payload");

            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.ToTable("settings");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Key, "settings_key_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.Key)
                .HasMaxLength(191)
                .HasColumnName("key");

            entity.Property(e => e.Value)
                .HasColumnType("text")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Subuser>(entity =>
        {
            entity.ToTable("subusers");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ServerId, "subusers_server_id_foreign");

            entity.HasIndex(e => e.UserId, "subusers_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Permissions)
                .HasColumnType("json")
                .HasColumnName("permissions");

            entity.Property(e => e.ServerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("server_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Server)
                .WithMany(p => p.Subusers)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("subusers_server_id_foreign");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Subusers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("subusers_user_id_foreign");
        });

        modelBuilder.Entity<TasksLog>(entity =>
        {
            entity.ToTable("tasks_log");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Response)
                .HasColumnType("text")
                .HasColumnName("response");

            entity.Property(e => e.RunStatus)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("run_status");

            entity.Property(e => e.RunTime)
                .HasColumnType("timestamp")
                .HasColumnName("run_time");

            entity.Property(e => e.TaskId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("task_id");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Email, "users_email_unique")
                .IsUnique();

            entity.HasIndex(e => e.ExternalId, "users_external_id_index");

            entity.HasIndex(e => e.Username, "users_username_unique")
                .IsUnique();

            entity.HasIndex(e => e.Uuid, "users_uuid_unique")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.Email)
                .HasMaxLength(191)
                .HasColumnName("email");

            entity.Property(e => e.ExternalId)
                .HasMaxLength(191)
                .HasColumnName("external_id");

            entity.Property(e => e.Gravatar)
                .IsRequired()
                .HasColumnName("gravatar")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.Language)
                .HasMaxLength(5)
                .HasColumnName("language")
                .HasDefaultValueSql("'en'")
                .IsFixedLength();

            entity.Property(e => e.NameFirst)
                .HasMaxLength(191)
                .HasColumnName("name_first");

            entity.Property(e => e.NameLast)
                .HasMaxLength(191)
                .HasColumnName("name_last");

            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");

            entity.Property(e => e.RememberToken)
                .HasMaxLength(191)
                .HasColumnName("remember_token");

            entity.Property(e => e.RootAdmin)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("root_admin");

            entity.Property(e => e.TotpAuthenticatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("totp_authenticated_at");

            entity.Property(e => e.TotpSecret)
                .HasColumnType("text")
                .HasColumnName("totp_secret");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UseTotp)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("use_totp");

            entity.Property(e => e.Username)
                .HasMaxLength(191)
                .HasColumnName("username");

            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<UserSshKey>(entity =>
        {
            entity.ToTable("user_ssh_keys");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "user_ssh_keys_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");

            entity.Property(e => e.Fingerprint)
                .HasMaxLength(191)
                .HasColumnName("fingerprint");

            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");

            entity.Property(e => e.PublicKey)
                .HasColumnType("text")
                .HasColumnName("public_key");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserSshKeys)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_ssh_keys_user_id_foreign");
        });
    }
}