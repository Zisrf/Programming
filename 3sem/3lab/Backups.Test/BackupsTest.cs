using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test
{
    public class BackupsTest
    {
        [Fact]
        public void Test()
        {
            var fileSystem = new MemoryFileSystem();
            fileSystem.CreateDirectory("/test");
            using (fileSystem.CreateFile("/test/a.txt"))
            using (fileSystem.CreateFile("/test/b.txt"))
            using (fileSystem.CreateFile("/test/c.txt"))
            {
            }

            IRepository repository = new InMemoryRepository("/test", fileSystem);
            IStorageAlgorithm storageAlgorithm = new SplitStorageAlgorithm(new ZipArchiver());
            var backupTask = new BackupTask("test_task", repository, storageAlgorithm);

            backupTask.AddBackupObject(new BackupObject("a.txt", repository));
            backupTask.AddBackupObject(new BackupObject("b.txt", repository));
            backupTask.AddBackupObject(new BackupObject("c.txt", repository));
            RestorePoint restorePoint = backupTask.CreateRestorePoint();

            Assert.True(fileSystem.DirectoryExists($"/test/test_task/{restorePoint.Id}"));
            Assert.Equal(3, fileSystem.EnumeratePaths($"/test/test_task/{restorePoint.Id}").Count());
        }
    }
}
