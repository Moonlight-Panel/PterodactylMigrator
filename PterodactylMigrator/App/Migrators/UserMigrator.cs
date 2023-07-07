using PterodactylMigrator.App.Database.Contexts;
using PterodactylMigrator.App.Database.Entities.Pterodactyl;
using PterodactylMigrator.App.Helpers;
using PterodactylMigrator.App.Models;
using Serilog;

namespace PterodactylMigrator.App.Migrators;

public class UserMigrator
{
    public void Migrate()
    {
        using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
        {
            if (ml.Users.Any())
            {
                Log.Information("Users in moonlight table found. Skipping migration");
                return;
            }
        }

        using (var pt = new PterodactylContext(MigratorConfig.PterodactylDatabaseConfig))
        {
            var users = pt.Users.ToArray();

            ParallelHelper.ExecuteParallel(users, Migrate, 250);
        }
    }

    private void Migrate(User user)
    {
        var newUser = new Database.Entities.Moonlight.User()
        {
            Id = user.Id,
            Admin = user.RootAdmin == 1,
            Email = user.Email.ToLower(),
            Password = user.Password,
            FirstName = user.NameFirst ?? "change me",
            LastName = user.NameLast ?? "change me",
            CreatedAt = user.CreatedAt ?? DateTime.UtcNow,
            UpdatedAt = user.UpdatedAt ?? DateTime.UtcNow,
            TokenValidTime = DateTime.UtcNow.AddDays(-5),
            SupportPending = false,
            CurrentSubscription = null,
            Status = UserStatus.Unverified,
            SubscriptionDuration = 0,
            DiscordId = 0,
            TotpSecret = "",
            Address = "",
            City = "",
            Country = "",
            State = "",
            SubscriptionSince = DateTime.UtcNow,
            TotpEnabled = false
        };

        using (var ml = new MoonlightContext(MigratorConfig.MoonlightDatabaseConfig))
        {
            ml.Users.Add(newUser);
            Log.Information($"Migrated user: '{newUser.Email}'");

            ml.SaveChanges();
        }
    }
}