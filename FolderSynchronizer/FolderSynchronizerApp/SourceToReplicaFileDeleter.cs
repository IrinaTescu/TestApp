using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    internal class SourceToReplicaFileDeleter
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _sourceFolderPath;
        private readonly string _replicaFolderPath;
        public SourceToReplicaFileDeleter(string sourceFolderPath, string replicaFolderPath)
        {
            _sourceFolderPath = sourceFolderPath;
            _replicaFolderPath = replicaFolderPath;
            
        }

        public void Execute()
        {
            var sourceFiles = Directory.GetFiles(_sourceFolderPath, "*", SearchOption.AllDirectories);
            var replicaFiles = Directory.GetFiles(_replicaFolderPath, "*", SearchOption.AllDirectories);

            foreach (var replicaFile in replicaFiles)
            {
                string additionalPath = GetAdditionalPath(replicaFile);
                var sourceFile = Path.Combine(_sourceFolderPath, additionalPath);
                if (!File.Exists(sourceFile))
                {
                    DeleteFile(replicaFile);
                }
            }
        }

        private void DeleteFile(string replicaFile)
        {
            logger.Info("Delete file {replicaFile}", replicaFile);
            File.Delete(replicaFile);
        }

        private string GetAdditionalPath(string replicaFile)
        {
            string additionalPath = replicaFile.Substring(_replicaFolderPath.Length);
            if (additionalPath.StartsWith(Path.DirectorySeparatorChar))
            {
                additionalPath = additionalPath.Substring(1);
            }
            return additionalPath;
        }
    }
}
