namespace PterodactylMigrator.App.Models;

public enum SecurityLogType
{
    ManipulatedJwt,
    PathTransversal,
    SftpBruteForce,
    LoginFail
}