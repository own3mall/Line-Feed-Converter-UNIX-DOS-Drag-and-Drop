using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConvertFilesToFormat.Classes
{
    public class TextFileConversionUtilities
    {
        BackgroundWorker bg = null;
        FileConversionArgs args = null;
        FileLineFeedConverter fileLineFeedConverter;

        public TextFileConversionUtilities(FileLineFeedConverter lf, ref BackgroundWorker main, FileConversionArgs options)
        {
            // Set vars
            bg = main;
            args = options;
            fileLineFeedConverter = lf;

            // Init
            Init();
        }

        private void Init()
        {
            if (args != null) {

                if (args.BackupFiles)
                {
                    backupFilesToProcess();
                }

                if (args.LFMode == 0)
                {
                    convertToUnixLF(args.FilesToProcess);
                }
                else
                {
                    convertToWindowsCRLF(args.FilesToProcess);
                }
            }
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

                    // Maintain directory structure on copying
                    var parentFolders = args.FolderParents.Where(c => filePath.Contains(c)).ToList();
                    if (parentFolders.Any() && args.MaintainFolderStructureOnBackup)
                    {
                        string parentFolder = parentFolders.OrderByDescending(c=> c.Length).FirstOrDefault();
                        if(parentFolder != null){
                            // We found a match, so maintain folder structure
                            string pathWeCareAbout = Path.GetDirectoryName(filePath);
                            pathWeCareAbout = pathWeCareAbout.Substring(parentFolder.Length);

                            // Append parent folder to the name
                            string rootFolderName = parentFolder.Substring(parentFolder.LastIndexOf("\\") + 1);

                            if (!string.IsNullOrEmpty(rootFolderName))
                            {
                                // Create the directories in the copy we're making
                                Directory.CreateDirectory(pathToBackup + "\\" + rootFolderName + pathWeCareAbout);

                                // Set the full path
                                fullBKPath = pathToBackup + "\\" + rootFolderName + pathWeCareAbout + "\\" + justFileName;
                            }
                        }
                    }

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
                if (!FileFolderHelper.FileHasBinaryContent(file))
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
                }
                else
                {
                    Console.WriteLine("Skipping file \"" + file + "\" due to binary content detection.\n");
                }

                count++;
                fileLineFeedConverter.totalFilesProcessed++;

                percentCount = (int)((fileLineFeedConverter.totalFilesProcessed / fileLineFeedConverter.totalFilesToProcess) * 100);
                fileLineFeedConverter.updateProgressbar(percentCount);
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
                if (!FileFolderHelper.FileHasBinaryContent(file))
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
                }
                else
                {
                    Console.WriteLine("Skipping file \"" + file + "\" due to binary content detection.\n");
                }
                count++;
                fileLineFeedConverter.totalFilesProcessed++;

                percentCount = (int)((fileLineFeedConverter.totalFilesProcessed / fileLineFeedConverter.totalFilesToProcess) * 100);
                fileLineFeedConverter.updateProgressbar(percentCount);
            }
        }
    }
}
