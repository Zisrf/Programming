using System.IO.Compression;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;
using Backups.Visitors;
using Backups.ZipObjects;

namespace Backups.Archivers;

public class ZipArchiver : IArchiver
{
    public IStorage CreateArchive(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path)
    {
        var storageId = Guid.NewGuid();
        string archiveName = $"{repository.PathCombine(path, storageId.ToString())}.zip";
        using Stream archiveStream = repository.OpenWrite(archiveName);
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Update);

        var visitor = new ZipVisitor(archive);
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
            repositoryObject.Accept(visitor);

        return new ZipStorage(new FolderZipObject(storageId.ToString(), visitor.ZipObjects), archiveName, repository);
    }
}