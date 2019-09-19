using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dsafa.WpfColorPicker
{
    /// <summary>
    /// Interaction logic for EyeDropperWindow.xaml
    /// </summary>
    internal partial class EyeDropperWindow : Window
    {
        private readonly EyeDropperWindowAdorner _adorner;
        private BitmapImage _image;

        public EyeDropperWindow(BitmapImage image)
        {
            InitializeComponent();
            SetImage(image);
            _adorner = new EyeDropperWindowAdorner(windowImage);
        }

        public Color SelectedColor { get; private set; }

        public void SetScreenRect(Rect screenRect)
        {
            // Apparently, the window cannot be maximized when we want to move it to a different screen.
            // Thanks to: http://www.codewrecks.com/blog/index.php/2013/01/05/open-a-window-in-fullscreen-on-a-specific-monitor-in-wpf/
            WindowState = WindowState.Normal;
            Left = screenRect.Left;
            Top = screenRect.Top;
            Width = screenRect.Width;
            Height = screenRect.Height;
        }

        public void SetImage(BitmapImage image)
        {
            windowImage.Source = image;
            _image = image;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var pos = e.GetPosition(this);
            _adorner.Color = GetColorAtPoint(pos);
            _adorner.SurroundingPixels = GetSurrounding(pos);
            _adorner.Position = pos;
        }

        private void WindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
        }

        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            _adorner.Position = Mouse.GetPosition(this);
            AdornerLayer.GetAdornerLayer(windowImage).Add(_adorner);
        }

        private void WindowOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            DialogResult = true;
            SelectedColor = _adorner.Color;
            Close();
        }

        private Color GetColorAtPoint(Point point)
        {
            var x = (int)point.X;
            var y = (int)point.Y;

            byte[] buffer = new byte[4];
            _image.CopyPixels(new Int32Rect(x, y, 1, 1), buffer, 4, 0);
            return Color.FromArgb(buffer[3], buffer[2], buffer[1], buffer[0]);
        }

        private ImageSource GetSurrounding(Point point)
        {
            var x = (int)point.X;
            var y = (int)point.Y;

            const int numSurroundingPixels = 7;
            const int length = numSurroundingPixels * 2;

            // clamp x and y so there is always room on the edge
            x = Math.Min(Math.Max(numSurroundingPixels + 1, x), (int)ActualWidth - numSurroundingPixels - 1);
            y = Math.Min(Math.Max(numSurroundingPixels + 1, y), (int)ActualHeight - numSurroundingPixels - 1);

            // create array that will fit the surrounding pixels
            byte[] buffer = new byte[length * length * 4];
            var stride = length * 4;
            _image.CopyPixels(new Int32Rect(x - numSurroundingPixels, y - numSurroundingPixels, length, length), buffer, stride, 0);
            return BitmapSource.Create(length, length, 96, 96, PixelFormats.Pbgra32, null, buffer, stride);
        }
    }
}
