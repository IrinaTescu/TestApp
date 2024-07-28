using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    public class ReplicaFolder
    {
        public string Path { get; set; }

        public ReplicaFolder(string path)
        {
            Path = path;
        }


    }
}
