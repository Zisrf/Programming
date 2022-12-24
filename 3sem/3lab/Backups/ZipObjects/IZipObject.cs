using System.IO.Compression;
using Backups.RepositoryObjects;

namespace Backups.ZipObjects;

public interface IZipObject
{
    string Name { get; }

    IRepositoryObject ToRepositoryObject(ZipArchiveEntry entry);
}