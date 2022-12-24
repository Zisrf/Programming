using Backups.Visitors;

namespace Backups.RepositoryObjects;

public class FolderRepositoryObject : IFolderRepositoryObject
{
    private readonly Func<IEnumerable<IRepositoryObject>> _getContentFunc;

    public FolderRepositoryObject(string name, Func<IEnumerable<IRepositoryObject>> getContentFunc)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(getContentFunc);

        Name = name;
        _getContentFunc = getContentFunc;
    }

    public string Name { get; }

    public IEnumerable<IRepositoryObject> GetContent()
    {
        return _getContentFunc.Invoke();
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}