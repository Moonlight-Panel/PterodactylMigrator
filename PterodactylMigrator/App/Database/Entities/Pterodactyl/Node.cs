namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Node
{
    public Node()
    {
        Allocations = new HashSet<Allocation>();
        DatabaseHosts = new HashSet<DatabaseHost>();
        Servers = new HashSet<Server>();
    }

    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public ushort Public { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int LocationId { get; set; }
    public string Fqdn { get; set; } = null!;
    public string Scheme { get; set; } = null!;
    public bool BehindProxy { get; set; }
    public bool MaintenanceMode { get; set; }
    public int Memory { get; set; }
    public int MemoryOverallocate { get; set; }
    public int Disk { get; set; }
    public int DiskOverallocate { get; set; }
    public int UploadSize { get; set; }
    public string DaemonTokenId { get; set; } = null!;
    public string DaemonToken { get; set; } = null!;
    public ushort DaemonListen { get; set; }
    public ushort DaemonSftp { get; set; }
    public string DaemonBase { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Location Location { get; set; } = null!;
    public virtual ICollection<Allocation> Allocations { get; set; }
    public virtual ICollection<DatabaseHost> DatabaseHosts { get; set; }
    public virtual ICollection<Server> Servers { get; set; }
}