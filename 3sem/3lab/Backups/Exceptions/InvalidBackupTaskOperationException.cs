using Backups.Entities;

namespace Backups.Exceptions;

public class InvalidBackupTaskOperationException : BackupsDomainException
{
    private InvalidBackupTaskOperationException(string? message)
        : base(message) { }

    public static InvalidBackupTaskOperationException OnAddExistingBackupObject(BackupTask backupTask, BackupObject backupObject)
        => new InvalidBackupTaskOperationException($"{backupTask} already contains {backupObject}");

    public static InvalidBackupTaskOperationException OnRemoveNonExistentBackupObject(BackupTask backupTask, BackupObject backupObject)
        => new InvalidBackupTaskOperationException($"{backupTask} doesn't contain {backupObject}");
}