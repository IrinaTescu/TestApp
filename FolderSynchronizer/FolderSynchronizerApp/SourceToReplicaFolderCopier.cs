using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    internal class SourceToReplicaFolderCopier
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _sourceFolderPath;
        private readonly string _replicaFolderPath;
        public SourceToReplicaFolderCopier(string sourceFolderPath, string replicaFolderPath)
        {
            _sourceFolderPath = sourceFolderPath;
            _replicaFolderPath = replicaFolderPath;
        }

        public void Execute()
        {
            var sourceDirectory = Directory.GetDirectories(_sourceFolderPath, "*", SearchOption.AllDirectories);
            var replicaDirectory = Directory.GetDirectories(_replicaFolderPath, "*", SearchOption.AllDirectories);
            foreach (var sourceDirectoryEntry in sourceDirectory)
            {
                string additionalPath = GetAdditionalPath(sourceDirectoryEntry);
                var destinationPath = Path.Combine(_replicaFolderPath, additionalPath);
                if (!replicaDirectory.Contains(destinationPath))
                {
                    CreateDirectoryIfNotExist(destinationPath);
                }
            }


        }

        private static void CreateDirectoryIfNotExist(string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
            {
                Logger.Info("Create Directory {DestinationPath}", destinationPath);
                Directory.CreateDirectory(destinationPath);
            }
        }

        private string GetAdditionalPath(string sourceDirectoryEntry)
        {
            string additionalPath = sourceDirectoryEntry.Substring(_sourceFolderPath.Length);
            if (additionalPath.StartsWith(Path.DirectorySeparatorChar))
            {
                additionalPath = additionalPath.Substring(1);
            }

            return additionalPath;
        }

    }
}
