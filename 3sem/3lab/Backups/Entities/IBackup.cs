namespace Backups.Entities;

public interface IBackup
{
    IEnumerable<RestorePoint> RestorePoints { get; }

    public void AddRestorePoint(RestorePoint restorePoint);
    public void RemoveRestorePoint(RestorePoint restorePoint);
}