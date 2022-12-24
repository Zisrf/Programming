using Backups.RepositoryObjects;

namespace Backups.Visitors;

public interface IVisitor
{
    void Visit(IFileRepositoryObject fileRepositoryObject);
    void Visit(IFolderRepositoryObject folderRepositoryObject);
}