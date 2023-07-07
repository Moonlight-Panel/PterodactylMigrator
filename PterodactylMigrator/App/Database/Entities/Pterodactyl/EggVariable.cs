namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class EggVariable
{
    public EggVariable()
    {
        ServerVariables = new HashSet<ServerVariable>();
    }

    public int Id { get; set; }
    public int EggId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string EnvVariable { get; set; } = null!;
    public string DefaultValue { get; set; } = null!;
    public byte UserViewable { get; set; }
    public byte UserEditable { get; set; }
    public string? Rules { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Egg Egg { get; set; } = null!;
    public virtual ICollection<ServerVariable> ServerVariables { get; set; }
}