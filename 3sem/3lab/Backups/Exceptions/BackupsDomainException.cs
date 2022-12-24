namespace Backups.Exceptions;

public class BackupsDomainException : Exception
{
    protected BackupsDomainException(string? message)
        : base(message) { }
}
