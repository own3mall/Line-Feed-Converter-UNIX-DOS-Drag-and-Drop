using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ConvertFilesToFormat.Classes
{
    public class TextFileConversionUtilities
    {
        BackgroundWorker bg = null;
        FileConversionArgs args = null;
        public TextFileConversionUtilities(ref BackgroundWorker main, FileConversionArgs options)
        {
            bg = main;
            args = options;

            Init();
        }

        private void Init()
        {
            if (args != null) {
                // Filter out any entries that don't end with the proper extension
                filterListOfFilesPerExtensionAndContent();

                if (args.BackupFiles)
                {
                    backupFilesToProcess();
                }

                if ( args.LFMode == 0)
                {
                    convertToUnixLF(args.FilesToProcess);
                }
                else
                {
                    convertToWindowsCRLF(args.FilesToProcess);
                }
            }
        }

        private void filterListOfFilesPerExtensionAndContent()
        {
            List<string> editedFilePaths = new List<string>();
        
            // Check file extensions on each file
            if (args.FileExtensionsToProcess.Any())
            {
                foreach (string filePath in args.FilesToProcess)
                {
                    var ext = Path.GetExtension(filePath);
                    if (string.IsNullOrEmpty(ext))
                    {
                        // No extension is fine too
                        // If we don't have an extension, check to see if it has binary content - ignore files that have binary content... we're just processing text files
                        if (!FileFolderHelper.FileHasBinaryContent(filePath))
                        {
                            editedFilePaths.Add(filePath);
                        }
                    }
                    else
                    {
                        if (args.FileExtensionsToProcess.Contains(ext))
                        {
                            editedFilePaths.Add(filePath);
                        }
                    }
                }
            }
            else
            {
                // User hasn't defined any extensions, which is fine, but we run into problems processing every file type...
                // Check for only files that don't have binary content
                foreach (string filePath in args.FilesToProcess)
                {
                    if (!FileFolderHelper.FileHasBinaryContent(filePath))
                    {
                        editedFilePaths.Add(filePath);
                    }
                }
            }

            // Set our filtered list
            args.FilesToProcess = editedFilePaths;
        }

        private void backupFilesToProcess()
        {
            if (Directory.Exists(args.BackupPath)) {
                DateTime date = DateTime.Now;
                string dateStr = date.ToString("M_dd_yyyy_H_mm_ss");
                string pathToBackup = args.BackupPath + "\\" + dateStr;

                if (!Directory.Exists(pathToBackup)) {
                    Directory.CreateDirectory(pathToBackup);
                }

                foreach (string filePath in args.FilesToProcess)
                {
                    string justFileName = Path.GetFileName(filePath);
                    string fullBKPath = pathToBackup + "\\" + justFileName;
                    if (File.Exists(fullBKPath)) {
                        fullBKPath += "_bk_" + GenericHelper.GenerateRandomStr();
                    }
                    File.Copy(filePath, fullBKPath);
                }
            }
        }

        public void convertToWindowsCRLF(List<string> filePaths)
        {
            int count = 0;
            int percentCount = 0;
            double totalFilesToProcess = Convert.ToDouble(filePaths.Count);
            foreach (string file in filePaths)
            {
                try
                {
                    string[] lines = File.ReadAllLines(file);
                    List<string> list_of_string = new List<string>();
                    foreach (string line in lines)
                    {
                        list_of_string.Add(line.Replace("\n", "\r\n"));
                    }
                    File.WriteAllLines(file, list_of_string);
                    Console.WriteLine("Converted file \"" + file + "\" to Windows CRLF DOS mode successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to convert file \"" + file + "\" to Windows CRLF DOS mode due to the following error:\n" + e.Message + "\n");
                }
                count++;

                percentCount = (int)((count / totalFilesToProcess) * 100);
                bg.ReportProgress(percentCount);

                if (percentCount >= 100)
                {
                    Console.WriteLine("Processed " + count.ToString() + " file(s) based on extensions and binary content filters.");
                }
            }
        }

        public void convertToUnixLF(List<string> filePaths)
        {
            int count = 0;
            int percentCount = 0;
            double totalFilesToProcess = Convert.ToDouble(filePaths.Count);
            const byte CR = 0x0D;
            const byte LF = 0x0A;
            foreach (string file in filePaths)
            {
                try
                {
                    byte[] data = File.ReadAllBytes(file);
                    using (FileStream fileStream = File.OpenWrite(file))
                    {
                        BinaryWriter bw = new BinaryWriter(fileStream);
                        int position = 0;
                        int index = 0;
                        do
                        {
                            index = Array.IndexOf<byte>(data, CR, position);
                            if ((index >= 0) && (data[index + 1] == LF))
                            {
                                // Write before the CR
                                bw.Write(data, position, index - position);
                                // from LF
                                position = index + 1;
                            }
                        }
                        while (index >= 0);
                        bw.Write(data, position, data.Length - position);
                        fileStream.SetLength(fileStream.Position);
                    }
                    Console.WriteLine("Converted file \"" + file + "\" to Unix LF mode successfully.");
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Failed to convert file \"" + file + "\" to Unix LF mode due to the following error:\n" + Ex.Message + "\n");
                }
                count++;

                percentCount = (int)((count / totalFilesToProcess) * 100);
                bg.ReportProgress(percentCount);

                if (percentCount >= 100)
                {
                    Console.WriteLine("Processed " + count.ToString() + " file(s) based on extensions and binary content filters.");
                }

            }
        }
    }
}
