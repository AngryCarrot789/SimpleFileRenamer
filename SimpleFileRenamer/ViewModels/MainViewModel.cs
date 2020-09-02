using Ookii.Dialogs.Wpf;
using SimpleFileRenamer.Explorer;
using SimpleFileRenamer.Files;
using SimpleFileRenamer.Utilities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace SimpleFileRenamer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<FileItemViewModel> Files { get; set; }

        private FileItemViewModel _selectedFile;
        public FileItemViewModel SelectedFile
        {
            get => _selectedFile;
            set => RaisePropertyChanged(ref _selectedFile, value, UpdateFileSelected);
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => RaisePropertyChanged(ref _selectedIndex, value, UpdateFileSelected);
        }

        private void UpdateFileSelected()
        {
            FileSelected = SelectedFile != null;

            if (SelectedFile != null)
            {
                HighlightFileNameCallback?.Invoke(Path.GetFileNameWithoutExtension(SelectedFile.FilePath));
            }
        }

        private bool _fileSelected;
        public bool FileSelected
        {
            get => _fileSelected;
            set => RaisePropertyChanged(ref _fileSelected, value);
        }

        public ICommand OpenDirectoryCommand { get; }

        public Action<string> HighlightFileNameCallback { get; set; }

        public MainViewModel()
        {
            Files = new ObservableCollection<FileItemViewModel>();

            OpenDirectoryCommand = new Command(OpenDirectory);
        }

        public void OpenDirectory()
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = "Select a folder, and all files in the folder will be imported";

            if (fbd.ShowDialog() == true)
            {
                ClearFiles();

                foreach (FileItemViewModel file in Fetcher.GetFiles(fbd.SelectedPath))
                {
                    SetupFileItemCallbacks(file);
                    AddFile(file);
                }
            }
        }

        public void SetupFileItemCallbacks(FileItemViewModel item)
        {
            item.RemoveCallback = RemoveFile;
            item.FileRenamedCallback = RemoveAndMoveToNextFile;
        }

        public void RemoveAndMoveToNextFile(FileItemViewModel file)
        {
            if (Files.Count > 1)
            {
                SelectedIndex++;
            }
            RemoveFile(file);
        }

        public void AddFile(FileItemViewModel file)
        {
            Files.Add(file);
        }

        public void RemoveFile(FileItemViewModel file)
        {
            Files.Remove(file);
        }

        public void ClearFiles()
        {
            Files.Clear();
        }
    }
}
