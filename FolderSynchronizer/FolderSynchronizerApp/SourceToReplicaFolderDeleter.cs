using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    internal class SourceToReplicaFolderDeleter
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _sourceFolderPath;
        private readonly string _replicaFolderPath;

        public SourceToReplicaFolderDeleter(string sourceFolderPath, string replicaFolderPath)
        {
            _sourceFolderPath = sourceFolderPath;
            _replicaFolderPath = replicaFolderPath;
        }

        public void Execute()
        {
            var sourceDirectory = Directory.GetDirectories(_sourceFolderPath, "*", SearchOption.AllDirectories);
            var replicaDirectory = Directory.GetDirectories(_replicaFolderPath, "*", SearchOption.AllDirectories);
            foreach (var replicaDirectoryEntry in replicaDirectory)
            {
                string additionalPath = GetAdditionalPath(replicaDirectoryEntry);
                var sourcePath = Path.Combine(_sourceFolderPath, additionalPath);
                if (!Directory.Exists(sourcePath))
                {
                    DeleteDirectory(replicaDirectoryEntry);
                    logger.Info("Delete Folder {replicaDirectoryEntry}", replicaDirectoryEntry);
                }
            }
        }

        private void DeleteDirectory(string replicaDirectoryEntry)
        {
            Directory.Delete(replicaDirectoryEntry, true);
        }

        private string GetAdditionalPath(string replicaDirectoryEntry)
        {
            string additionalPath = replicaDirectoryEntry.Substring(_replicaFolderPath.Length);
            if (additionalPath.StartsWith(Path.DirectorySeparatorChar))
            {
                additionalPath = additionalPath.Substring(1);
            }
            return additionalPath;
        }
    }
}
