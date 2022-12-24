using Backups.Exceptions;
using Backups.RepositoryObjects;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    public InMemoryRepository(string rootPath, MemoryFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(rootPath);
        ArgumentNullException.ThrowIfNull(fileSystem);

        RootPath = rootPath;
        FileSystem = fileSystem;

        if (!fileSystem.DirectoryExists(rootPath))
            fileSystem.CreateDirectory(rootPath);
    }

    public string RootPath { get; }
    public MemoryFileSystem FileSystem { get; }

    public string GetFullPath(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        return UPath.Combine(RootPath, path).ToString();
    }

    public string PathCombine(params string[] paths)
    {
        ArgumentNullException.ThrowIfNull(paths);

        return UPath.Combine(paths.Select(p => new UPath(p)).ToArray()).ToString();
    }

    public Stream OpenWrite(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);

        string fullFilePath = GetFullPath(filePath);
        string? directoryName = Path.GetDirectoryName(fullFilePath);

        if (directoryName is not null && !FileSystem.DirectoryExists(directoryName))
            FileSystem.CreateDirectory(directoryName);

        return FileSystem.CreateFile(fullFilePath);
    }

    public bool IsRepositoryObjectExists(string objectPath)
    {
        ArgumentNullException.ThrowIfNull(objectPath);

        string fullObjectPath = GetFullPath(objectPath);

        return FileSystem.FileExists(fullObjectPath) || FileSystem.DirectoryExists(fullObjectPath);
    }

    public IRepositoryObject GetRepositoryObject(string objectPath)
    {
        ArgumentNullException.ThrowIfNull(objectPath);

        string fullObjectPath = GetFullPath(objectPath);
        string objectName = Path.GetFileName(fullObjectPath);

        if (!IsRepositoryObjectExists(objectPath))
            throw InvalidRepositoryOperationException.OnGetNonExistentRepositoryObject(objectPath);

        if (FileSystem.FileExists(fullObjectPath))
        {
            return new FileRepositoryObject(objectName, () => FileSystem.OpenFile(fullObjectPath, FileMode.Open, FileAccess.Read));
        }
        else if (FileSystem.DirectoryExists(fullObjectPath))
        {
            return new FolderRepositoryObject(
                objectName,
                () => FileSystem.EnumeratePaths(fullObjectPath).Select(p => GetRepositoryObject(p.ToString())));
        }
        else
        {
            throw InvalidRepositoryOperationException.OnGetNonExistentRepositoryObject(objectPath);
        }
    }
}