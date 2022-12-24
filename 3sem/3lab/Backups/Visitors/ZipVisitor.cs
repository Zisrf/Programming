using System.IO.Compression;
using Backups.RepositoryObjects;
using Backups.ZipObjects;

namespace Backups.Visitors;

public class ZipVisitor : IVisitor
{
    private readonly Stack<ZipArchive> _archives;
    private readonly Stack<List<IZipObject>> _zipObjects;

    public ZipVisitor(ZipArchive archive)
    {
        ArgumentNullException.ThrowIfNull(archive);

        _archives = new Stack<ZipArchive>();
        _zipObjects = new Stack<List<IZipObject>>();

        _archives.Push(archive);
        _zipObjects.Push(new List<IZipObject>());
    }

    public IEnumerable<IZipObject> ZipObjects => _zipObjects.Peek();

    public void Visit(IFileRepositoryObject fileRepositoryObject)
    {
        using Stream zipStream = _archives.Peek().CreateEntry(fileRepositoryObject.Name).Open();
        using Stream fileStream = fileRepositoryObject.OpenRead();
        fileStream.CopyTo(zipStream);

        _zipObjects.Peek().Add(new FileZipObject(fileRepositoryObject.Name));
    }

    public void Visit(IFolderRepositoryObject folderRepositoryObject)
    {
        Stream stream = _archives.Peek().CreateEntry($"{folderRepositoryObject.Name}.zip").Open();
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        _archives.Push(archive);

        _zipObjects.Push(new List<IZipObject>());
        foreach (IRepositoryObject repositoryObject in folderRepositoryObject.GetContent())
            repositoryObject.Accept(this);

        var folder = new FolderZipObject($"{folderRepositoryObject.Name}.zip", _zipObjects.Pop());
        _zipObjects.Peek().Add(folder);
        _archives.Pop();
    }
}