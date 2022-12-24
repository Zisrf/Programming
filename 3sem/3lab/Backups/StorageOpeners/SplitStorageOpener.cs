using Backups.RepositoryObjects;

namespace Backups.StorageOpeners;

public class SplitStorageOpener : IStorageOpener
{
    private IEnumerable<IStorageOpener> _openers;

    public SplitStorageOpener(IEnumerable<IStorageOpener> openers)
    {
        ArgumentNullException.ThrowIfNull(openers);

        _openers = openers;
    }

    public void Dispose()
    {
        foreach (IStorageOpener opener in _openers)
            opener.Dispose();
    }

    public IEnumerable<IRepositoryObject> GetRepositoryObjects()
    {
        return _openers.SelectMany(o => o.GetRepositoryObjects());
    }
}