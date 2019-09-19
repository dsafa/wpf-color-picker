using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace Dsafa.WpfColorPicker
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

        public static Rectangle CurrentScreenRect()
        {
            return System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position).Bounds;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var window = new EyeDropperWindow(ScreenshotBitmapImage());
            // The EyeDropperWindow spans the entire current screen, so this event gets called whenever
            // the mouse changes from one screen to another.
            window.MouseLeave += (_s, _e) =>
            {
                // Changing the screen rect and image unfortunately causes a short flash of the old
                // image on the new screen. To circumvent this issue, we first set the window to be
                // invisible (with a size of 0x0.
                // This is not ideal but makes the issue less noticable. Actually hiding the window
                // seems to work better but we can't do that because calling `.Hide()` or setting
                // `.Visibility` sets the `DialogResult` to `false`.
                window.SetScreenRect(new Rectangle(0, 0, 0, 0));
                window.SetImage(ScreenshotBitmapImage());
                window.SetScreenRect(CurrentScreenRect());
            };
            window.SetScreenRect(CurrentScreenRect());
            var res = window.ShowDialog();
            if (res.HasValue && res.Value)
            {
                SelectedColor = window.SelectedColor;
            }
        }

        private BitmapImage ScreenshotBitmapImage()
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

            return bitmapImage;
        }

        private Bitmap TakeScreenshot()
        {
            Rectangle screen = CurrentScreenRect();
            var screenBitmap = new Bitmap(screen.Width, screen.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(screenBitmap))
            {
                g.CopyFromScreen(screen.Left, screen.Top, 0, 0, screenBitmap.Size);
            }

            return screenBitmap;
        }
    }
}
