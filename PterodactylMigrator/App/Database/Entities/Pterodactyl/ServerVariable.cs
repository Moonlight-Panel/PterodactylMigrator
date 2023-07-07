namespace PterodactylMigrator.App.Database.Entities.Pterodactyl;

public partial class ServerVariable
{
    public int Id { get; set; }
    public int? ServerId { get; set; }
    public int VariableId { get; set; }
    public string VariableValue { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Server? Server { get; set; }
    public virtual EggVariable Variable { get; set; } = null!;
}