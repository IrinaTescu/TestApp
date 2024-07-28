namespace FolderSynchronizerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceFolderPath = @"C:\Playground\Source";
            string replicaFolderPath = @"C:\Playground\Replica";
            var sourceToReplicaFileCopier = new SourceToReplicaFileCopier(sourceFolderPath, replicaFolderPath);
            sourceToReplicaFileCopier.Execute();

        }

        
    }
}

