using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                    files.AddRange(DirSearch(d));
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
