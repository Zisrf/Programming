using System.IO.Compression;
using Backups.RepositoryObjects;

namespace Backups.ZipObjects;

public class FileZipObject : IZipObject
{
    public FileZipObject(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject ToRepositoryObject(ZipArchiveEntry entry)
    {
        return new FileRepositoryObject(Name, () => entry.Open());
    }
}