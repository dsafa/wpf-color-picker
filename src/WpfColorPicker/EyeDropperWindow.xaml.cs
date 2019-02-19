using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for EyeDropperWindow.xaml
    /// </summary>
    internal partial class EyeDropperWindow : Window
    {
        private readonly EyeDropperWindowAdorner _adorner;
        private byte[] _pixels;
        private int _stride;
        private int _height;

        public EyeDropperWindow(BitmapImage image)
        {
            InitializeComponent();
            windowImage.Source = image;
            _adorner = new EyeDropperWindowAdorner(windowImage);

            _stride = image.PixelWidth * 4;
            _height = image.PixelHeight;
            _pixels = new byte[_height * _stride];
            image.CopyPixels(_pixels, _stride, 0);

            Loaded += EyeDropperWindowOnLoaded;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var pos = e.GetPosition(this);
            _adorner.Color = GetColorAtPoint(pos);
            _adorner.Position = pos;
        }

        private void WindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void EyeDropperWindowOnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(windowImage).Add(_adorner);
        }

        private Color GetColorAtPoint(Point point)
        {
            var x = (int)point.X;
            var y = (int)point.Y;

            var index = y * _stride + (x * 4);
            var b = _pixels[index];
            var g = _pixels[index + 1];
            var r = _pixels[index + 2];
            var a = _pixels[index + 3];
            return Color.FromArgb(a, r, g, b);
        }
    }
}
