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
            var sourceToReplicaFileCopier = new SourceToReplicaFileCopier(sourceFolderPath, replicaFolderPath);
            sourceToReplicaFileCopier.Execute();

        }

        
    }
}

