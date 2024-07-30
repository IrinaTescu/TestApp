using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    internal class Synchronizer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _sourceFolderPath;
        private readonly string _replicaFolderPath;
        public Synchronizer(string sourcePath, string replicaPath)
        {
            _sourceFolderPath = sourcePath;
            _replicaFolderPath = replicaPath;
        }
        public void Synchronize()
        {
            Logger.Info("Copy files from {Source} to {Replica}", _sourceFolderPath, _replicaFolderPath);
            var sourceToReplicaFolder = new SourceToReplicaFolderCopier(_sourceFolderPath, _replicaFolderPath);
            var sourceToReplicaFileCopier = new SourceToReplicaFileCopier(_sourceFolderPath, _replicaFolderPath, new MD5Calculator());
            sourceToReplicaFolder.Execute();
            sourceToReplicaFileCopier.Execute();

            Logger.Info("Delete folder from {Relica}", _replicaFolderPath);
            var sourceToReplicaFileDeleter = new SourceToReplicaFileDeleter(_sourceFolderPath, _replicaFolderPath);
            var sourceToReplicaFolderDeleter = new SourceToReplicaFolderDeleter(_sourceFolderPath, _replicaFolderPath);
            sourceToReplicaFileDeleter.Execute();
            sourceToReplicaFolderDeleter.Execute();
        }
    }
}
