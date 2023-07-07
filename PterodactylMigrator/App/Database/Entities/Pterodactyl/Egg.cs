namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class Egg
{
    public Egg()
    {
        EggVariables = new HashSet<EggVariable>();
        InverseConfigFromNavigation = new HashSet<Egg>();
        InverseCopyScriptFromNavigation = new HashSet<Egg>();
        Servers = new HashSet<Server>();
    }

    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public int NestId { get; set; }
    public string Author { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Features { get; set; }
    public string? DockerImages { get; set; }
    public string? FileDenylist { get; set; }
    public string? UpdateUrl { get; set; }
    public string? ConfigFiles { get; set; }
    public string? ConfigStartup { get; set; }
    public string? ConfigLogs { get; set; }
    public string? ConfigStop { get; set; }
    public int? ConfigFrom { get; set; }
    public string? Startup { get; set; }
    public string ScriptContainer { get; set; } = null!;
    public int? CopyScriptFrom { get; set; }
    public string ScriptEntry { get; set; } = null!;
    public bool? ScriptIsPrivileged { get; set; }
    public string? ScriptInstall { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Egg? ConfigFromNavigation { get; set; }
    public virtual Egg? CopyScriptFromNavigation { get; set; }
    public virtual Nest Nest { get; set; } = null!;
    public virtual ICollection<EggVariable> EggVariables { get; set; }
    public virtual ICollection<Egg> InverseConfigFromNavigation { get; set; }
    public virtual ICollection<Egg> InverseCopyScriptFromNavigation { get; set; }
    public virtual ICollection<Server> Servers { get; set; }
}