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
                var additionalPath=Path.GetDirectoryName(sourceFile).Substring(sourceFolderPath.Length);
                if(additionalPath.StartsWith(Path.DirectorySeparatorChar))
                {

                   additionalPath=additionalPath.Substring(1);
                }
                var destinationPath = Path.Combine(replicaFolderPath,additionalPath, Path.GetFileName(sourceFile));
                if (!replicaFiles.Contains(destinationPath))
                {
                    if(!Directory.Exists(Path.GetDirectoryName(destinationPath)))
                    {

                       Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    }
                    File.Copy(sourceFile, destinationPath);
                }
            }
            
            
            

        }
    }
}
