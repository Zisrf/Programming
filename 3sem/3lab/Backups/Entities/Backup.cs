using Backups.Exceptions;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly HashSet<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new HashSet<RestorePoint>();
    }

    public IEnumerable<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        if (!_restorePoints.Add(restorePoint))
            throw InvalidBackupOperationException.OnAddExistingRestorePoint(this, restorePoint);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        if (!_restorePoints.Remove(restorePoint))
            throw InvalidBackupOperationException.OnRemoveNonExistentRestorePoint(this, restorePoint);
    }
}
