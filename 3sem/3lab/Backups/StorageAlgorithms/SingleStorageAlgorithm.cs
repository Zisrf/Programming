using Backups.Archivers;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;

namespace Backups.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    private readonly IArchiver _archiver;

    public SingleStorageAlgorithm(IArchiver archiver)
    {
        ArgumentNullException.ThrowIfNull(archiver);

        _archiver = archiver;
    }

    public IStorage CreateStorage(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path)
    {
        return _archiver.CreateArchive(repositoryObjects, repository, path);
    }
}