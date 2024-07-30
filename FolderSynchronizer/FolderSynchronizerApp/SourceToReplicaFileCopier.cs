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
        private readonly IMD5Calculator _md5Calculator;
        public SourceToReplicaFileCopier(string sourceFolderPath, string replicaFolderPath, IMD5Calculator md5calculator)
        {
            _sourceFolderPath = sourceFolderPath;
            _replicaFolderPath = replicaFolderPath;
            _md5Calculator = md5calculator;
        }

        public void Execute()
        {
            
            var sourceFiles = Directory.GetFiles(_sourceFolderPath, "*", SearchOption.AllDirectories);
            var replicaFiles = Directory.GetFiles(_replicaFolderPath, "*", SearchOption.AllDirectories);

            foreach (var sourceFile in sourceFiles)
            {
                string additionalPath = GetAdditionalPath(sourceFile);
                var destinationPath = Path.Combine(_replicaFolderPath, additionalPath, Path.GetFileName(sourceFile));
                if (!replicaFiles.Contains(destinationPath))
                {
                    CreateDirectoryIfNotExist(destinationPath);
                    Logger.Info("Copy {SourceFile} to {DestinationPath}", sourceFile, destinationPath);
                    File.Copy(sourceFile, destinationPath);
                }
                else
                {
                    var sourceMD5=_md5Calculator.Calculate(sourceFile);
                    var destinationMD5=_md5Calculator.Calculate(destinationPath);
                    if(sourceMD5!=destinationMD5)
                    {
                        Logger.Info("Update {SourceFile} to {DestinationPath}", sourceFile, destinationPath);
                        File.Copy(sourceFile, destinationPath,true);
                    }
                }
            }
        }

        private void CreateDirectoryIfNotExist(string destinationPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
            {
                Logger.Info("Create Directory {DestinationPath}", destinationPath);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
            }
        }

        private string GetAdditionalPath(string sourceFile)
        {
            var additionalPath = Path.GetDirectoryName(sourceFile).Substring(_sourceFolderPath.Length);
            if (additionalPath.StartsWith(Path.DirectorySeparatorChar))
            {
                additionalPath = additionalPath.Substring(1);
            }

            return additionalPath;
        }

    }
}
