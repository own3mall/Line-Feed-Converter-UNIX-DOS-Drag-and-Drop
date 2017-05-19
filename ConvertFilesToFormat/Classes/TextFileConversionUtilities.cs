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
                bg.ReportProgress((int)((count / filePaths.Count) * 100));
            }
        }

        public void convertToUnixLF(List<string> filePaths)
        {
            int count = 0;
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
                bg.ReportProgress((int)((count / filePaths.Count) * 100));
            }
        }
    }
}
