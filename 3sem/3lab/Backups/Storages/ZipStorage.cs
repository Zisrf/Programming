using Backups.Repositories;
using Backups.StorageOpeners;
using Backups.ZipObjects;

namespace Backups.Storages;

public class ZipStorage : IStorage
{
    private readonly FolderZipObject _rootFolder;

    public ZipStorage(FolderZipObject rootFolder, string path, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(rootFolder);
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(repository);

        _rootFolder = rootFolder;
        Path = path;
        Repository = repository;
    }

    public string Path { get; }
    public IRepository Repository { get; }

    public IStorageOpener Open()
    {
        return new ZipStorageOpener(_rootFolder, Repository, Path);
    }
}