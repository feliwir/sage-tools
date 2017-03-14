using Microsoft.Win32;
using sage.vp6;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace vp6play
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Demuxer m_demuxer;
        WriteableBitmap m_bitmap;
        Timer m_timer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = ""
            if (ofd.ShowDialog() == true)
            {
                m_demuxer = new Demuxer(File.Open(ofd.FileName,FileMode.Open,FileAccess.Read));

                var width = m_demuxer.Video.Width;
                var height = m_demuxer.Video.Height;
                var format = PixelFormats.Rgb24;
                var buffer = m_demuxer.GetBufferRGB();

                m_bitmap = new WriteableBitmap((int)width, (int)height, 96d, 96d,
                    format, null);

                Int32Rect rec = new Int32Rect(0, 0, (int)width, (int)height);

                m_bitmap.WritePixels(rec, buffer, m_bitmap.BackBufferStride,0);
                ui_image.Source = m_bitmap;

                
            }
        }
    }
}
