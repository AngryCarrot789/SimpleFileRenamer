using SimpleFileRenamer.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Input;

namespace SimpleFileRenamer.Files
{
    public class FileItemViewModel : BaseViewModel
    {
        private Icon _icon;
        public Icon Icon
        {
            get => _icon;
            set => RaisePropertyChanged(ref _icon, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => RaisePropertyChanged(ref _filePath, value, UpdateOldFileName);
        }

        private void UpdateOldFileName()
        {
            if (File.Exists(FilePath))
                OldFileName = Path.GetFileName(FilePath);
        }

        private string _oldFileName;
        public string OldFileName
        {
            get => _oldFileName;
            set => RaisePropertyChanged(ref _oldFileName, value);
        }

        private string _newFileName;
        public string NewFileName
        {
            get => _newFileName;
            set => RaisePropertyChanged(ref _newFileName, value);
        }

        private long _fileSize;
        public long FileSize
        {
            get => _fileSize;
            set => RaisePropertyChanged(ref _fileSize, value);
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => RaisePropertyChanged(ref _isReadOnly, value);
        }

        public ICommand RemoveFileCommand { get; }
        public ICommand RenameFileCommand { get; set; }

        public Action<FileItemViewModel> RemoveCallback { get; set; }
        public Action<FileItemViewModel> FileRenamedCallback { get; set; }

        public FileItemViewModel()
        {
            RemoveFileCommand = new Command(RemoveFile);
            RenameFileCommand = new Command(RenameFile);
        }

        public FileItemViewModel(FileInfo info) : this()
        {
            FilePath = info.FullName;
            OldFileName = info.Name;
            NewFileName = OldFileName;
            FileSize = info.Length;
            IsReadOnly = info.IsReadOnly;
        }

        public void RenameFile()
        {
            string originalName = Path.GetFileName(FilePath);
            string newName = NewFileName;
            string newPath = FilePath.ReplaceLastOccurrence(originalName, newName);

            if (File.Exists(FilePath))
            {
                File.Move(FilePath, newPath);
            }

            FileRenamedCallback?.Invoke(this);
        }

        public void RemoveFile()
        {
            RemoveCallback?.Invoke(this);
        }
    }
}
