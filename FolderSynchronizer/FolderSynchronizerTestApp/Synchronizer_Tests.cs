using FolderSynchronizerApp;

namespace FolderSynchronizerTestApp
{
    public class Synchronizer_Tests
    {
        private string _sourceFolderPath;
        private string _replicaFolderPath;

        [SetUp]
        public void Setup()
        {
            _sourceFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            _replicaFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(_sourceFolderPath);
            Directory.CreateDirectory(_replicaFolderPath);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_sourceFolderPath, true);
            Directory.Delete(_replicaFolderPath, true);
        }

        [Test]
        public void OneFileAddedToSource_AfterSync_ReplicaContainsTheSameContent()
        {
            //Arrange
            var content = Guid.NewGuid().ToString();
            File.WriteAllText(Path.Combine(_sourceFolderPath, "File.txt"),content);

            //Act
            Synchronizer synchronizer = new Synchronizer(_sourceFolderPath, _replicaFolderPath);
            synchronizer.Synchronize();

            //Assert
            var replicaFile = Path.Combine(_replicaFolderPath, "File.txt");
            Assert.That(File.Exists(replicaFile));
            Assert.That(File.ReadAllText(replicaFile), Is.EqualTo(content));
        }
        [Test]
        public void OneFileDeletedFromSource_AfterSync_ReplicaDoesNotContainTheFile()
        {
            //Arrange
            var content = Guid.NewGuid().ToString();
            var sourceFile = Path.Combine(_sourceFolderPath, "File.txt");
            File.WriteAllText(sourceFile, content);

            //Act
            Synchronizer synchronizer = new Synchronizer(_sourceFolderPath, _replicaFolderPath);
            synchronizer.Synchronize();

            File.Delete(sourceFile);
            synchronizer.Synchronize();

            //Assert
            var replicaFile = Path.Combine(_replicaFolderPath, "File.txt");
            Assert.That(!File.Exists(replicaFile));
        }
        [Test]
        public void OneFileUpdatedInSource_AfterSync_ReplicaContainsTheUpdatedContent()
        {
            //Arrange
            var content = Guid.NewGuid().ToString();
            var sourceFile = Path.Combine(_sourceFolderPath, "File.txt");
            File.WriteAllText(sourceFile, content);

            //Act
            Synchronizer synchronizer = new Synchronizer(_sourceFolderPath, _replicaFolderPath);
            synchronizer.Synchronize();

            var updatedContent = Guid.NewGuid().ToString();
            File.WriteAllText(sourceFile, updatedContent);
            synchronizer.Synchronize();

            //Assert
            var replicaFile = Path.Combine(_replicaFolderPath, "File.txt");
            Assert.That(File.Exists(replicaFile));
            Assert.That(File.ReadAllText(replicaFile), Is.EqualTo(updatedContent));
        }
        [Test]
        public void OneFileAddedToReplica_AfterSync_FileIsDeletedFromReplica()
        {
            //Arrange
            var content = Guid.NewGuid().ToString();
            var replicaFile = Path.Combine(_replicaFolderPath, "File.txt");
            File.WriteAllText(replicaFile, content);

            //Act
            Synchronizer synchronizer = new Synchronizer(_sourceFolderPath, _replicaFolderPath);
            synchronizer.Synchronize();

            //Assert
            Assert.That(!File.Exists(replicaFile));
        }

    }
}