using Backups.RepositoryObjects;

namespace Backups.StorageOpeners;

public interface IStorageOpener : IDisposable
{
    IEnumerable<IRepositoryObject> GetRepositoryObjects();
}