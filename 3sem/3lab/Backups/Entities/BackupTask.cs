using Backups.Exceptions;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.StorageAlgorithms;
using Backups.Storages;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly HashSet<BackupObject> _backupObjects;

    public BackupTask(string name, IRepository repository, IStorageAlgorithm storageAlgorithm)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(storageAlgorithm);

        Name = name;
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;

        Backup = new Backup();
        _backupObjects = new HashSet<BackupObject>();
    }

    public string Name { get; }
    public Backup Backup { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }

    public IEnumerable<BackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        if (!_backupObjects.Add(backupObject))
            throw InvalidBackupTaskOperationException.OnAddExistingBackupObject(this, backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        if (!_backupObjects.Remove(backupObject))
            throw InvalidBackupTaskOperationException.OnRemoveNonExistentBackupObject(this, backupObject);
    }

    public RestorePoint CreateRestorePoint()
    {
        var restorePointId = Guid.NewGuid();
        string restorePointPath = Repository.PathCombine(Name, $"{restorePointId}");

        IEnumerable<IRepositoryObject> repositoryObjects =
            _backupObjects.Select(o => o.Repository.GetRepositoryObject(o.Path));

        IStorage storage = StorageAlgorithm.CreateStorage(repositoryObjects, Repository, restorePointPath);
        var restorePoint = new RestorePoint(restorePointId, DateTime.Now, storage, _backupObjects.ToList());
        Backup.AddRestorePoint(restorePoint);

        return restorePoint;
    }
}
