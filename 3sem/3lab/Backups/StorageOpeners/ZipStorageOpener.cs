using System.IO.Compression;
using Backups.Exceptions;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.ZipObjects;

namespace Backups.StorageOpeners;

public class ZipStorageOpener : IStorageOpener
{
    private readonly Stream _archiveStream;
    private readonly FolderZipObject _rootArchiveFolder;

    public ZipStorageOpener(FolderZipObject rootArchiveFolder, IRepository repository, string path)
    {
        ArgumentNullException.ThrowIfNull(rootArchiveFolder);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(path);

        if (repository.GetRepositoryObject(path) is not IFileRepositoryObject file)
            throw InvalidRepositoryOperationException.OnGetNonExistentRepositoryObject(path);

        Repository = repository;
        Path = path;
        _rootArchiveFolder = rootArchiveFolder;

        _archiveStream = file.OpenRead();
    }

    public IRepository Repository { get; }
    public string Path { get; }

    public IEnumerable<IRepositoryObject> GetRepositoryObjects()
    {
        using var archive = new ZipArchive(_archiveStream, ZipArchiveMode.Read);

        return archive.Entries.Select(entry => _rootArchiveFolder.Content.First(o
            => o.Name.Equals(entry.Name)).ToRepositoryObject(entry)).ToList();
    }

    public void Dispose()
    {
        _archiveStream.Dispose();
    }
}