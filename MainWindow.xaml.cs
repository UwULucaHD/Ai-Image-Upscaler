using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Ai_Image_Upscaler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? _imgLocation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var height = int.Parse(Height.Text.ToString());
            var width = int.Parse(Width.Text.ToString());
            var path = _imgLocation;
            // var path = Path.Text.ToString();
            //var output = Output.Text.ToString();
            ScaleImage(height, width, path);
        }

        private void ScaleImage(int height, int width, string path)
        {
            var indexOfDot = path.IndexOf('.');
            var output = path.Insert(indexOfDot, "UpscaledImage");
            Task.Run(() =>
            {
                MagicImageProcessor.ProcessImage(path, output, new ProcessImageSettings
                {
                    Height = height,
                    Width = width,
                    ResizeMode = CropScaleMode.Contain
                });
            });
        }

        private void DropPath_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    placeholderText.Visibility = Visibility.Collapsed;
                    _imgLocation = files[0]; // Get the path of the dropped image

                    //// Display the dropped image in the label
                    BitmapImage bitmap = new BitmapImage(new Uri(_imgLocation));
                    ImagePreview.Source = bitmap;
                }
            }
        }
    }
}
