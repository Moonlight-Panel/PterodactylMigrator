namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Server
{
    public Server()
    {
        Allocations = new HashSet<Allocation>();
        AuditLogs = new HashSet<AuditLog>();
        Backups = new HashSet<Backup>();
        Databases = new HashSet<Database>();
        Schedules = new HashSet<Schedule>();
        ServerTransfers = new HashSet<ServerTransfer>();
        ServerVariables = new HashSet<ServerVariable>();
        Subusers = new HashSet<Subuser>();
    }

    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public Guid Uuid { get; set; }
    public string UuidShort { get; set; } = null!;
    public int NodeId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Status { get; set; }
    public bool SkipScripts { get; set; }
    public int OwnerId { get; set; }
    public int Memory { get; set; }
    public int Swap { get; set; }
    public int Disk { get; set; }
    public int Io { get; set; }
    public int Cpu { get; set; }
    public string? Threads { get; set; }
    public byte OomDisabled { get; set; }
    public int AllocationId { get; set; }
    public int NestId { get; set; }
    public int EggId { get; set; }
    public string Startup { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int? AllocationLimit { get; set; }
    public int? DatabaseLimit { get; set; }
    public int BackupLimit { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Allocation? Allocation { get; set; } = null!;
    public virtual Egg Egg { get; set; } = null!;
    public virtual Nest? Nest { get; set; } = null!;
    public virtual Node Node { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
    public virtual ICollection<Allocation> Allocations { get; set; }
    public virtual ICollection<AuditLog> AuditLogs { get; set; }
    public virtual ICollection<Backup> Backups { get; set; }
    public virtual ICollection<Database> Databases { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
    public virtual ICollection<ServerTransfer> ServerTransfers { get; set; }
    public virtual ICollection<ServerVariable> ServerVariables { get; set; }
    public virtual ICollection<Subuser> Subusers { get; set; }
}