using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;

namespace Backups.StorageAlgorithms;

public interface IStorageAlgorithm
{
    IStorage CreateStorage(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path);
}