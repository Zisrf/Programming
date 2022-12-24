using Backups.Entities;

namespace Backups.Exceptions;

public class InvalidBackupOperationException : BackupsDomainException
{
    private InvalidBackupOperationException(string? message)
        : base(message) { }

    public static InvalidBackupOperationException OnAddExistingRestorePoint(Backup backup, RestorePoint restorePoint)
        => new InvalidBackupOperationException($"{backup} already contains {restorePoint}");

    public static InvalidBackupOperationException OnRemoveNonExistentRestorePoint(Backup backup, RestorePoint restorePoint)
        => new InvalidBackupOperationException($"{backup} doesn't contain {restorePoint}");
}