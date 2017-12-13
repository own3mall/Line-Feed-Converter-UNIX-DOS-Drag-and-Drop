using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConvertFilesToFormat.Classes
{
    public static class GenericHelper
    {
        public static Random random = new Random();

        public static void SynchronizedInvoke(this ISynchronizeInvoke sync, Action action)
        {
            // If the invoke is not required, then invoke here and get out.
            if (!sync.InvokeRequired)
            {
                // Execute action.
                action();

                // Get out.
                return;
            }

            // Marshal to the required context.
            sync.Invoke(action, new object[] { });
        }

        public static void SafeInvoke(this Control control, Action handler)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(handler);
            }
            else
            {
                handler();
            }
        }

        public static string GenerateRandomStr(int length = 10)
        {
            string finalStr = "";
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (int i = 0; i < length; i++)
            {
                finalStr += chars[random.Next(chars.Length)];
            }

            return finalStr;
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }


        public static List<string> SplitCommaStringToList(string blackListedExtensionsStr)
        {
            List<string> result = new List<string>();
            result = blackListedExtensionsStr.Split(',').ToList();
            result.RemoveAll(c => string.IsNullOrEmpty(c));
            return result;
        }

        public static List<string> FilterFiles(List<string> Files, List<string> FileExtensionsToProcess, List<string> BlackListedFileExtensions)
        {

            List<string> editedFilePaths = new List<string>();

            // Check file extensions on each file
            if (FileExtensionsToProcess.Any())
            {
                foreach (string filePath in Files)
                {
                    var ext = Path.GetExtension(filePath);
                    if (string.IsNullOrEmpty(ext))
                    {
                        editedFilePaths.Add(filePath);
                    }
                    else
                    {
                        if (FileExtensionsToProcess.Contains(ext) && !BlackListedFileExtensions.Contains(ext))
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
                foreach (string filePath in Files)
                {
                    var ext = Path.GetExtension(filePath);
                    if (string.IsNullOrEmpty(ext))
                    {
                        editedFilePaths.Add(filePath);
                    }
                    else
                    {
                        // Check our blacklisted extensions list...
                        if (!BlackListedFileExtensions.Contains(ext))
                        {
                            editedFilePaths.Add(filePath);
                        }
                    }
                }
            }

            // Set our filtered list
            return editedFilePaths;
        }
    }
}
