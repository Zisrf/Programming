using Backups.Visitors;

namespace Backups.RepositoryObjects;

public class FileRepositoryObject : IFileRepositoryObject
{
    private readonly Func<Stream> _openFunc;

    public FileRepositoryObject(string name, Func<Stream> openFunc)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(openFunc);

        Name = name;
        _openFunc = openFunc;
    }

    public string Name { get; }

    public Stream OpenRead()
    {
        return _openFunc.Invoke();
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}