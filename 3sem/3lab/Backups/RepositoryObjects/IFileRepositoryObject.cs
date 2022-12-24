namespace Backups.RepositoryObjects;

public interface IFileRepositoryObject : IRepositoryObject
{
    Stream OpenRead();
}