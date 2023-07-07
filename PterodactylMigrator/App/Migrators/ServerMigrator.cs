using Microsoft.EntityFrameworkCore;
using PterodactylMigrator.App.Database.Contexts;
using PterodactylMigrator.App.Database.Entities.Moonlight;
using PterodactylMigrator.App.Helpers;
using PterodactylMigrator.App.Models;
using Serilog;

namespace PterodactylMigrator.App.Migrators;

public class ServerMigrator
{
    public void Migrate()
    {
        using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
        {
            if (ml.Servers.Any())
            {
                Log.Information("Servers in moonlight table found. Skipping migration");
                return;
            }

            using (var pt = new PterodactylContext(MigratorConfig.PterodactylDatabaseConfig))
            {
                var servers = pt.Servers.ToArray();

                ParallelHelper.ExecuteParallel(servers, Migrate, 500);
            }
        }
    }

    private void Migrate(PterodactylMigrator.App.Database.Entities.Pterodactyl.Server s)
    {
        using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
        {
            using (var pt = new PterodactylContext(MigratorConfig.PterodactylDatabaseConfig))
            {
                var server = pt.Servers
                    .Include(x => x.Allocation)
                    .Include(x => x.Allocations)
                    .Include(x => x.Backups)
                    .Include(x => x.Egg)
                    .Include(x => x.Node)
                    .Include(x => x.Owner)
                    .First(x => x.Id == s.Id);

                var newServer = new Server()
                {
                    Id = server.Id,
                    Node = ml.Nodes.First(x => x.Id == server.Node.Id),
                    Owner = ml.Users.First(x => x.Id == server.Owner.Id),
                    Image = ml.Images.First(x => x.Id == server.Egg.Id),
                    Cpu = server.Cpu,
                    Disk = server.Disk,
                    Installing = false,
                    Memory = server.Memory,
                    Name = server.Name,
                    Suspended = server.Status == "suspended",
                    Uuid = server.Uuid,
                    MainAllocation = ml.NodeAllocations.First(x => x.Id == server.Allocation!.Id),
                    IsCleanupException = false
                };
                
                // Startup check

                newServer.OverrideStartup = server.Startup == newServer.Image.Startup ? "" : server.Startup;
                
                // Docker image index calculation
                
                var imageIndex = -1;
                var image = ml.Images
                    .Include(x => x.DockerImages)
                    .Include(x => x.Variables)
                    .First(x => x.Id == newServer.Image.Id);

                if (image.DockerImages.Any(x => x.Name == server.Image))
                {
                    imageIndex = image.DockerImages.FindIndex(x => x.Name == server.Image);
                }

                if (imageIndex == -1)
                {
                    Log.Debug($"Server '{server.Name}' has a docker image which is not in the docker image collection of the corresponding image ('{server.Image}'). Resetting to default image");
                    imageIndex = image.DockerImages.FindIndex(x => x.Default);
                }

                newServer.DockerImageIndex = imageIndex;
                
                // Server variables
                
                var serverVars = pt.ServerVariables
                    .Where(x => x.ServerId == server.Id)
                    .Include(x => x.Variable)
                    .ToArray();
                
                foreach (var variable in image.Variables)
                {
                    var serverVar = serverVars
                        .FirstOrDefault(x => x.Variable.EnvVariable == variable.Key);

                    if (serverVar == null)
                    {
                        Log.Information($"'{server.Name}' is missing server variable '{variable.Key}' specified in the image '{image.Name}'. Adding it with default values");
                        
                        newServer.Variables.Add(new ()
                        {
                            Key = variable.Key,
                            Value = variable.DefaultValue
                        });
                    }
                    else
                    {
                        newServer.Variables.Add(new()
                        {
                            Key = variable.Key,
                            Value = serverVar.VariableValue
                        });
                    }
                }
                
                // Server backups
                
                foreach (var backup in server.Backups)
                {
                    var newBackup = new ServerBackup()
                    {
                        Id = (int)backup.Id,
                        Name = backup.Name,
                        Uuid = backup.Uuid,
                        CreatedAt = backup.CreatedAt ?? DateTime.UtcNow,
                        Bytes = backup.Bytes,
                        Created = true
                    };
                    
                    newServer.Backups.Add(newBackup);
                }
                
                foreach (var allocation in server.Allocations)
                {
                    var nodeAllo = ml.NodeAllocations.First(y => y.Id == allocation.Id);
                    newServer.Allocations.Add(nodeAllo);
                }

                ml.Servers.Add(newServer);
                ml.SaveChanges();
                
                Log.Information($"Migrated server: '{newServer.Name}'");
            }
        }
    }
}