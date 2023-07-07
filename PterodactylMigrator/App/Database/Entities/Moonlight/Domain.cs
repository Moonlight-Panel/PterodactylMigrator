using System.ComponentModel.DataAnnotations;

namespace PterodactylMigrator.App.Database.Entities.Moonlight;

public class Domain
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    public SharedDomain SharedDomain { get; set; }
    public User Owner { get; set; }
}