using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConvertFilesToFormat.Classes
{
    public static class FileFolderHelper
    {
        public static List<string> DirSearch(string sDir)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    // Get directory info
                    DirectoryInfo dir = new DirectoryInfo(d);

                    // Depending on our settings, we may filter some of these
                    if ((!Path.GetFileName(d).StartsWith(".") || ConfigurationManager.AppSettings["SkipFoldersThatBeginWithPeriod"] == "0") && (!dir.Attributes.HasFlag(FileAttributes.Hidden) || ConfigurationManager.AppSettings["SkipHiddenFolders"] == "0"))
                    {
                        files.AddRange(DirSearch(d));
                    }
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return files;
        }

        public static bool FileHasBinaryContent(string filePath)
        {
            string content = File.ReadAllText(filePath);
            char[] allowedChars = {'\r', '\n', '\t', '\f', '\v', '\b'};
            return content.Any(ch => char.IsControl(ch) && !allowedChars.Contains(ch));
        }
    }
}
