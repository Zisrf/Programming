namespace Backups.RepositoryObjects;

public interface IFolderRepositoryObject : IRepositoryObject
{
    IEnumerable<IRepositoryObject> GetContent();
}