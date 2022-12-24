using Backups.StorageOpeners;

namespace Backups.Storages;

public interface IStorage
{
    IStorageOpener Open();
}