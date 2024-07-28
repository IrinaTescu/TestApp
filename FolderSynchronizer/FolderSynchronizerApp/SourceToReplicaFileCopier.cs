using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    internal class SourceToReplicaFileCopier
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _sourceFolderPath;
        private readonly string _replicaFolderPath;
        public SourceToReplicaFileCopier(string sourceFolderPath, string replicaFolderPath)
        {
            _sourceFolderPath = sourceFolderPath;
            _replicaFolderPath = replicaFolderPath;
        }

        public void Execute()
        {
            var sourceFiles = Directory.GetFiles(_sourceFolderPath, "*", SearchOption.AllDirectories);
            var replicaFiles = Directory.GetFiles(_replicaFolderPath, "*", SearchOption.AllDirectories);

            foreach (var sourceFile in sourceFiles)
            {
                var additionalPath = Path.GetDirectoryName(sourceFile).Substring(_sourceFolderPath.Length);
                if (additionalPath.StartsWith(Path.DirectorySeparatorChar))
                {
                    additionalPath = additionalPath.Substring(1);
                }
                var destinationPath = Path.Combine(_replicaFolderPath, additionalPath, Path.GetFileName(sourceFile));
                if (!replicaFiles.Contains(destinationPath))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
                    {
                        Logger.Info("Create Directory {DestinationPath}", destinationPath);
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    }
                    Logger.Info("Copy {SourceFile} to {DestinationPath}", sourceFile, destinationPath);
                    File.Copy(sourceFile, destinationPath);
                }
            }
        }
    }
}
