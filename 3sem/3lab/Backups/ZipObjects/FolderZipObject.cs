using System.IO.Compression;
using Backups.RepositoryObjects;

namespace Backups.ZipObjects;

public class FolderZipObject : IZipObject
{
    public FolderZipObject(string name, IEnumerable<IZipObject> content)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(content);

        Name = name;
        Content = content;
    }

    public string Name { get; }
    public IEnumerable<IZipObject> Content { get; }

    public IRepositoryObject ToRepositoryObject(ZipArchiveEntry entry)
    {
        using var archive = new ZipArchive(entry.Open());
        var repositoryObjects = archive.Entries.Select(archiveEntry
            => Content.First(o => o.Name.Equals(archiveEntry.Name)).ToRepositoryObject(archiveEntry)).ToList();

        return new FolderRepositoryObject(Name, () => repositoryObjects);
    }
}