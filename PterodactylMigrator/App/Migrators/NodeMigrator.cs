using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using PterodactylMigrator.App.Database.Contexts;
using PterodactylMigrator.App.Database.Entities.Pterodactyl;
using PterodactylMigrator.App.Models;
using Serilog;

namespace PterodactylMigrator.App.Migrators;

public class NodeMigrator
{
    public void Migrate()
    {
        using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
        {
            if (ml.Nodes.Any())
            {
                Log.Information("Nodes in moonlight table found. Skipping migration");
                return;
            }
        }

        using (var pt = new PterodactylContext(MigratorConfig.PterodactylDatabaseConfig))
        {
            using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
            {
                foreach (var node in pt.Nodes.ToArray())
                {
                    var newNode = new Database.Entities.Moonlight.Node()
                    {
                        Id = node.Id,
                        Fqdn = node.Fqdn,
                        Name = node.Name,
                        Ssl = node.Scheme == "https",
                        Token = GetKey(node),
                        HttpPort = node.DaemonListen,
                        SftpPort = node.DaemonSftp,
                        TokenId = node.DaemonTokenId,
                        MoonlightDaemonPort = 9999
                    };

                    var allocations = pt.Allocations
                        .Where(x => x.NodeId == node.Id)
                        .ToArray();

                    foreach (var allocation in allocations)
                    {
                        newNode.Allocations.Add(new()
                        {
                            Port = allocation.Port,
                            Id = allocation.Id
                        });
                    }

                    ml.Nodes.Add(newNode);
                    ml.SaveChanges();
                    Log.Information($"Migrated node: '{node.Name}'");
                }
            }
        }
    }

    public string GetKey(Node node)
    {
        var jsonByte = Convert.FromBase64String(node.DaemonToken);
        var json = Encoding.UTF8.GetString(jsonByte);

        var data = new ConfigurationBuilder().AddJsonStream(
            new MemoryStream(Encoding.ASCII.GetBytes(json!))
        ).Build();

        var iv = data.GetValue<string>("iv");
        var value = data.GetValue<string>("value");

        var appSecret = MigratorConfig.AppKey;

        byte[] cipherText = Convert.FromBase64String(value);
        byte[] key = Convert.FromBase64String(appSecret);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = Convert.FromBase64String(iv);

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipherText);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        var decryptedText = sr.ReadToEnd();

        return decryptedText.Replace("s:64:\"", "").Replace("\";", "");
    }
}