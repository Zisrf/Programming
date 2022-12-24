using Backups.Exceptions;
using Backups.RepositoryObjects;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string rootPath)
    {
        ArgumentNullException.ThrowIfNull(rootPath);

        RootPath = rootPath;

        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);
    }

    public string RootPath { get; }

    public string GetFullPath(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        return Path.Combine(RootPath, path);
    }

    public string PathCombine(params string[] paths)
    {
        ArgumentNullException.ThrowIfNull(paths);

        return Path.Combine(paths);
    }

    public Stream OpenWrite(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);

        string fullFilePath = GetFullPath(filePath);
        string? directoryName = Path.GetDirectoryName(fullFilePath);

        if (directoryName is not null && !Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);

        return File.Create(fullFilePath);
    }

    public bool IsRepositoryObjectExists(string objectPath)
    {
        ArgumentNullException.ThrowIfNull(objectPath);

        string fullObjectPath = GetFullPath(objectPath);

        return File.Exists(fullObjectPath) || Directory.Exists(fullObjectPath);
    }

    public IRepositoryObject GetRepositoryObject(string objectPath)
    {
        ArgumentNullException.ThrowIfNull(objectPath);

        if (!IsRepositoryObjectExists(objectPath))
            throw InvalidRepositoryOperationException.OnGetNonExistentRepositoryObject(objectPath);

        string fullObjectPath = GetFullPath(objectPath);
        string objectName = Path.GetFileName(fullObjectPath);

        if (File.Exists(fullObjectPath))
        {
            return new FileRepositoryObject(objectName, () => File.OpenRead(fullObjectPath));
        }
        else if (Directory.Exists(fullObjectPath))
        {
            return new FolderRepositoryObject(
                objectName,
                () => Directory.EnumerateFileSystemEntries(fullObjectPath).Select(p => GetRepositoryObject(p)));
        }
        else
        {
            throw InvalidRepositoryOperationException.OnGetNonExistentRepositoryObject(objectPath);
        }
    }
}