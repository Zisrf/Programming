using Backups.StorageOpeners;

namespace Backups.Storages;

public class SplitStorage : IStorage
{
    private IEnumerable<IStorage> _storages;

    public SplitStorage(IEnumerable<IStorage> storages)
    {
        ArgumentNullException.ThrowIfNull(storages);

        _storages = storages;
    }

    public IStorageOpener Open()
    {
        return new SplitStorageOpener(_storages.Select(s => s.Open()));
    }
}