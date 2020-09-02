using SimpleFileRenamer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleFileRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel Model
        {
            get => this.DataContext as MainViewModel;
            set => this.DataContext = value;
        }
        public MainWindow()
        {
            InitializeComponent();
            Model = new MainViewModel();
            Model.HighlightFileNameCallback = HighlightFileName;
        }

        private void HighlightFileName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                FileNameBox.Select(0, name.Length);
            }
        }
    }
}
