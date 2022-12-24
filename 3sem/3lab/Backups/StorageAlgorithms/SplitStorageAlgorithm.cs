using Backups.Archivers;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;

namespace Backups.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    private readonly IArchiver _archiver;

    public SplitStorageAlgorithm(IArchiver archiver)
    {
        ArgumentNullException.ThrowIfNull(archiver);

        _archiver = archiver;
    }

    public IStorage CreateStorage(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path)
    {
        var storages = repositoryObjects.Select(o =>
            _archiver.CreateArchive(new List<IRepositoryObject>() { o }, repository, path)).ToList();

        return new SplitStorage(storages);
    }
}