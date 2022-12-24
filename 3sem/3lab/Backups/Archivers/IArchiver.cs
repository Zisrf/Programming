using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;

namespace Backups.Archivers;

public interface IArchiver
{
    IStorage CreateArchive(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path);
}