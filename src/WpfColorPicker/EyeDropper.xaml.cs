using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for EyeDropper.xaml
    /// </summary>
    public partial class EyeDropper : UserControl
    {
        public static DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(EyeDropper), new PropertyMetadata(Colors.Red));

        public EyeDropper()
        {
            InitializeComponent();
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
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
            var res = window.ShowDialog();
            if (res.HasValue && res.Value)
            {
                SelectedColor = window.SelectedColor;
            }
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
