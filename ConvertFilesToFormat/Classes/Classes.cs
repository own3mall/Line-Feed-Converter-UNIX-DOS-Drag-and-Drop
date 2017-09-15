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
            // Set defaults
            FilesToProcess = new List<string>();
            FileExtensionsToProcess = new List<string>();
            MaintainFolderStructureOnBackup = true;
            NumberOfThreadsToUse = 10;
        }

        public int LFMode { get; set; }

        public string BackupPath { get; set; }

        public bool BackupFiles { get; set; }

        public List<string> FilesToProcess { get; set; }

        public List<string> FileExtensionsToProcess { get; set; }

        public List<string> FolderParents { get; set; }

        public int NumberOfThreadsToUse { get; set; }

        public bool MaintainFolderStructureOnBackup { get; set; }
    }

    [Serializable()]
    public class FileCollection
    {
        public FileCollection()
        {
            Files = new List<string>();
        }

        public string Type { get; set; }

        public string ParentDirectory { get; set; }

        public List<string> Files { get; set; }
    }
}
