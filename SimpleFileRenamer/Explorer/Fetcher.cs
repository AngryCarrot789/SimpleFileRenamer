using SimpleFileRenamer.Helpers;
using SimpleFileRenamer.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using FileExplorer.Explorer;

namespace SimpleFileRenamer.Explorer
{
    public static class Fetcher
    {
        public static List<FileItemViewModel> GetFiles(string directory)
        {
            List<FileItemViewModel> files = new List<FileItemViewModel>();

            if (!directory.IsDirectory())
                return files;

            // for exception handling
            string currentFile = "";

            // code for getting all files
            try
            {
                foreach (string file in Directory.GetFiles(directory))
                {
                    currentFile = file;
                    if (string.IsNullOrEmpty(file))
                        continue;

                    if (file.IsFile())
                    {
                        FileInfo fInfo = new FileInfo(file);
                        FileItemViewModel fModel = new FileItemViewModel()
                        {
                            Icon = IconHelper.GetIconOfFile(file, false, false),
                            OldFileName = fInfo.Name,
                            NewFileName = fInfo.Name,
                            FilePath = fInfo.FullName,
                            FileSize = fInfo.Length
                        };

                        files.Add(fModel);
                    }
                }

                return files;
            }

            catch (IOException io)
            {
                MessageBox.Show(
                    $"IO Exception getting files in directory: {io.Message}",
                    "Exception getting files in directory");
            }
            catch (UnauthorizedAccessException noAccess)
            {
                MessageBox.Show(
                    $"No access for a file: {noAccess.Message}",
                    "Exception getting files in directory");
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Failed to get files in '{directory}' || " +
                    $"Something to do with '{currentFile}'\n" +
                    $"Exception: {e.Message}", "Error");
            }

            return files;
        }
    }
}
