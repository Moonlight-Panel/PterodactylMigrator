using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PterodactylMigrator.App.Database.Contexts;
using PterodactylMigrator.App.Database.Entities.Moonlight;
using PterodactylMigrator.App.Models;
using Serilog;

namespace PterodactylMigrator.App.Migrators;

public class ImageMigrator
{
    public void Migrate()
    {
        using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
        {
            if (ml.Images.Any())
            {
                Log.Information("Images found in moonlight table. Skipping");
                return;
            }

            using (var pt = new PterodactylContext(MigratorConfig.PterodactylDatabaseConfig))
            {
                foreach (var egg in pt.Eggs.ToArray())
                {
                    var image = new Image()
                    {
                        Name = egg.Name,
                        Description = egg.Description ?? "",
                        Startup = egg.Startup ?? "",
                        ConfigFiles = egg.ConfigFiles ?? "{}",
                        InstallScript = egg.ScriptInstall ?? "",
                        InstallDockerImage = egg.ScriptContainer,
                        InstallEntrypoint = egg.ScriptEntry,
                        Id = egg.Id,
                        Uuid = egg.Uuid,
                        StopCommand = egg.ConfigStop ?? "",
                        TagsJson = "[]"
                    };

                    var data = new ConfigurationBuilder().AddJsonStream(
                        new MemoryStream(Encoding.ASCII.GetBytes(egg.ConfigStartup!))
                    ).Build();

                    image.StartupDetection = data.GetValue<string>("done", "") ?? "";

                    foreach (var eggVariable in pt.EggVariables.Where(x => x.EggId == egg.Id))
                    {
                        image.Variables.Add(new()
                        {
                            DefaultValue = eggVariable.DefaultValue,
                            Key = eggVariable.EnvVariable
                        });
                    }

                    var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(egg.DockerImages);

                    foreach (var dockerImage in dic)
                    {
                        if (image.DockerImages.All(x => x.Name != dockerImage.Value))
                        {
                            var di = new DockerImage()
                            {
                                Default = dockerImage.Key == dic.Last().Key,
                                Name = dockerImage.Value
                            };

                            image.DockerImages.Add(di);
                        }
                    }

                    try
                    {
                        ml.Images.Add(image);
                        ml.SaveChanges();

                        Log.Debug($"Migrated {image.Name}");
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Error migrating {image.Name}");
                        Log.Error(e.Message);
                    }
                }
            }
        }
    }
}