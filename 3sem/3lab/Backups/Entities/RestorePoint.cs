using Backups.Storages;

namespace Backups.Entities;

public class RestorePoint
{
    public RestorePoint(Guid id, DateTime date, IStorage storage, IEnumerable<BackupObject> backupObjects)
    {
        ArgumentNullException.ThrowIfNull(storage);
        ArgumentNullException.ThrowIfNull(backupObjects);

        Id = id;
        Date = date;
        Storage = storage;
        BackupObjects = backupObjects;
    }

    public Guid Id { get; }
    public DateTime Date { get; }
    public IStorage Storage { get; }
    public IEnumerable<BackupObject> BackupObjects { get; }
}