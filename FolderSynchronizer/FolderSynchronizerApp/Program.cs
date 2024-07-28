namespace FolderSynchronizerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceFolderPath = @"C:\Playground\Source";
            string replicaFolderPath = @"C:\Playground\Replica";

            var sourceFiles = Directory.GetFiles(sourceFolderPath,"*",SearchOption.AllDirectories);
            var replicaFiles = Directory.GetFiles(replicaFolderPath,"*",SearchOption.AllDirectories);

            foreach(var sourceFile in sourceFiles)
            {
                File.Copy(sourceFile,Path.Combine(replicaFolderPath,Path.GetFileName(sourceFile)));
            }
            


        }
    }
}
