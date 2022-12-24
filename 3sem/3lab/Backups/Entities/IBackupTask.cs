using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

public interface IBackupTask
{
    string Name { get; }
    Backup Backup { get; }
    IRepository Repository { get; }
    IStorageAlgorithm StorageAlgorithm { get; }
    IEnumerable<BackupObject> BackupObjects { get; }

    void AddBackupObject(BackupObject backupObject);
    void RemoveBackupObject(BackupObject backupObject);

    RestorePoint CreateRestorePoint();
}