using Backups.Exceptions;
using Backups.Repositories;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string path, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(repository);

        Path = path;
        Repository = repository;
    }

    public string Path { get; }
    public IRepository Repository { get; }
}
