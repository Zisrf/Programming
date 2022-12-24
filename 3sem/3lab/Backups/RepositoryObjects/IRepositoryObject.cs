using Backups.Visitors;

namespace Backups.RepositoryObjects;

public interface IRepositoryObject
{
    string Name { get; }

    void Accept(IVisitor visitor);
}