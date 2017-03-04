using Microsoft.Win32;
using sage.big;
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

                foreach(var entry in arch.Entries)
                {
                    Path.GetFullPath(entry.FullName);
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = entry.FullName;
                    ui_treeView.Items.Add(tvi);
                }
            }
        }
    }
}
