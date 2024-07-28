using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizerApp
{
    public class SourceFolder
    {
        public string Path { get; set; }
        public SourceFolder(string path)
        {
            Path = path;
        }
    }
}
