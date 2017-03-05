using Microsoft.Win32;
using sage.big;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace bigedit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel m_archiveTree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                BigArchive arch = new BigArchive(File.Open(ofd.FileName, FileMode.Open));

            }
        }   
    }

    public interface IFolder
    {
        string FullPath { get; }
        string FolderLabel { get; }
        List<IFolder> Folders { get; }
    }

    public class Folder : IFolder
    {
        public List<IFolder> Folders { get; set; }
        public string FolderLabel { get; set; }
        public string FullPath { get; set; }

        public Folder()
        {
            Folders = new List<IFolder>();
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            m_folders = new List<IFolder>();

          //somehow put BigArchive inside the ViewModel
            
        }

        private List<IFolder> m_folders;
        public List<IFolder> Folders
        {
            get { return m_folders; }
            set
            {
                m_folders = value;
                NotifiyPropertyChanged("Folders");
            }
        }

        void NotifiyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
