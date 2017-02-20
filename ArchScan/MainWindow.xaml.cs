using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.IO;

namespace ArchScan
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                //check if device is not available
                if (listBox.Items.Count == 0)
                {
                    MessageBox.Show("You do not have any WIA devices.");
                    this.Close();
                }
                else
                {
                    listBox.SelectedIndex = 0;
                }
                //get images from scanner
                List<Image> images = WIAScanner.Scan((string)listBox.SelectedItem);
                foreach (Image image in images)
                {
                    // ImageSource ...

                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    // Save to a memory stream...
                    image.Save(ms, ImageFormat.Bmp);
                    // Rewind the stream...
                    ms.Seek(0, SeekOrigin.Begin);
                    // Tell the WPF image to use this stream...
                    bi.StreamSource = ms;
                    bi.EndInit();


                    ImageBox.Source = bi;
                   
                    
                    //save scanned image into specific folder
                    //image.Save(@"D:\" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpeg", ImageFormat.Jpeg);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //get list of devices available
            List<string> devices = WIAScanner.GetDevices();

            foreach (string device in devices)
            {
                listBox.Items.Add(device);
            }
        }
    }
}
