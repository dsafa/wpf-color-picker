using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for EyeDropper.xaml
    /// </summary>
    public partial class EyeDropper : UserControl
    {
        public EyeDropper()
        {
            InitializeComponent();
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var screenshot = TakeScreenshot();

            var bitmapImage = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                screenshot.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Position = 0;

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
            }

            screenshot.Dispose();

            var window = new EyeDropperWindow(bitmapImage);
            window.Show();
        }

        private Bitmap TakeScreenshot()
        {
            var screenBitmap = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(screenBitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, screenBitmap.Size);
            }

            return screenBitmap;
        }
    }
}
