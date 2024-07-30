namespace FolderSynchronizerApp
{
    internal class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string sourceFolderPath = @"C:\Playground\Source";
            string replicaFolderPath = @"C:\Playground\Replica";
            Logger.Info("Copy files from {Source} to {Replica}",sourceFolderPath,replicaFolderPath);
            var sourceToReplicaFolder=new SourceToReplicaFolderCopier(sourceFolderPath,replicaFolderPath);
            var sourceToReplicaFileCopier = new SourceToReplicaFileCopier(sourceFolderPath, replicaFolderPath, new MD5Calculator());
            sourceToReplicaFolder.Execute();
            sourceToReplicaFileCopier.Execute();

            Logger.Info("Delete folder from {Relica}", replicaFolderPath);
            var sourceToReplicaFileDeleter = new SourceToReplicaFileDeleter(sourceFolderPath, replicaFolderPath);
            var sourceToReplicaFolderDeleter = new SourceToReplicaFolderDeleter(sourceFolderPath, replicaFolderPath);
            sourceToReplicaFileDeleter.Execute();
            sourceToReplicaFolderDeleter.Execute();


        }

        
    }
}

