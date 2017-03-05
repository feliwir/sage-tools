using Microsoft.Win32;
using sage.big;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

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

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(ui_filter.Text))
                return true;
            else
                return ((item as BigArchiveEntry).FullName.IndexOf(ui_filter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = ""
            if (ofd.ShowDialog() == true)
            {
                BigArchive arch = new BigArchive(File.Open(ofd.FileName, FileMode.Open));
                ui_listview.ItemsSource = arch.Entries;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ui_listview.ItemsSource);
                view.Filter = UserFilter;
            }
        }

        private void ui_listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BigArchiveEntry entry = ui_listview.SelectedItem as BigArchiveEntry;
            string extension = Path.GetExtension(entry.FullName);
           
            if(extension==".tga"|| extension == ".jpg" || extension == ".png" || extension ==".dds")
            {
                SetImage();
            }
            else
            {
                SetText();
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ui_listview.ItemsSource);
            view.Filter = UserFilter;
        }

        private void SetImage()
        {
            ui_content.Children.Clear();
            BigArchiveEntry entry = ui_listview.SelectedItem as BigArchiveEntry;
            Image img = new Image();
            img.Source = BitmapFrame.Create(entry.Open(), 
                                            BitmapCreateOptions.None,
                                            BitmapCacheOption.OnLoad);
            
            ui_content.Children.Add(img);
        }

        private void SetText()
        {
            ui_content.Children.Clear();
            BigArchiveEntry entry = ui_listview.SelectedItem as BigArchiveEntry;
            StreamReader sr = new StreamReader(entry.Open());
            ScrollViewer sv = new ScrollViewer();
            TextBox tb = new TextBox();
            tb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tb.Text = sr.ReadToEnd();
            ui_content.Children.Add(tb);
        }

        private void ui_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ui_listview.ItemsSource).Refresh();
        }
    }
   
}
