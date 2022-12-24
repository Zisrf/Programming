using Backups.RepositoryObjects;

namespace Backups.Repositories;

public interface IRepository
{
    string PathCombine(params string[] paths);

    Stream OpenWrite(string filePath);
    IRepositoryObject GetRepositoryObject(string objectPath);
}