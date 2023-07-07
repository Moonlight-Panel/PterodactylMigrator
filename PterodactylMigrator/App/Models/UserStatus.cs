namespace PterodactylMigrator.App.Models;

public enum UserStatus
{
    Unverified,
    Verified,
    VerifyPending,
    VerifyFailed,
    Warned,
    Banned,
    Disabled,
    DataPending, 
    PasswordPending
}