namespace Backups.Exceptions;

public class InvalidRepositoryOperationException : BackupsDomainException
{
    private InvalidRepositoryOperationException(string? message)
        : base(message) { }

    public static InvalidRepositoryOperationException OnGetNonExistentRepositoryObject(string objectPath)
        => new InvalidRepositoryOperationException($"Repository doesn't contain an object with path {objectPath}");
}