using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvertFilesToFormat.Classes
{
    [Serializable()]
    public class FileConversionArgs
    {
        public FileConversionArgs()
        {
            FilesToProcess = new List<string>();
            FileExtensionsToProcess = new List<string>();
        }

        public int LFMode { get; set; }

        public string BackupPath { get; set; }

        public bool BackupFiles { get; set; }

        public List<string> FilesToProcess { get; set; }

        public List<string> FileExtensionsToProcess { get; set; }
    }
}
