using System;
using System.Collections.Generic;
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
    /// Interaction logic for HuePicker.xaml
    /// </summary>
    public partial class HuePicker : UserControl
    {
        private static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(HuePicker));
        private readonly HuePickerAdorner _adorner;
        private byte[] _pixels;
        private int _stride;
        private MemoryStream _ms;

        public HuePicker()
        {
            InitializeComponent();
            _adorner = new HuePickerAdorner(hueRectangle);
            Loaded += HuePickerOnLoaded;
            SizeChanged += HuePickerOnSizeChanged;
        }

        private void HuePickerOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(this);
        }

        private void HuePickerOnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
            Snapshot();
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            UpdateAdorner(e.GetPosition(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            UpdateAdorner(e.GetPosition(this));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Snapshot();
        }

        private void UpdateAdorner(Point mousePos)
        {
            _adorner.VerticalPercent = mousePos.Y / ActualHeight;
            _adorner.Color = GetColor((int)mousePos.Y);
        }

        private void Snapshot()
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(hueRectangle);

            _ms?.Dispose();
            _ms = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(_ms);

            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = _ms;
            imageSource.EndInit();

            _stride = imageSource.PixelWidth * 4;
            int size = imageSource.PixelHeight * _stride;
            _pixels = new byte[size];
            imageSource.CopyPixels(_pixels, _stride, 0);
        }

        private Color GetColor(int y)
        {
            if (_pixels == null)
            {
                return Colors.Red;
            }

            int index = y * _stride;
            var b = _pixels[index];
            var g = _pixels[index + 1];
            var r = _pixels[index + 2];
            var a = _pixels[index + 3];
            return Color.FromArgb(a, r, g, b);
        }
    }
}
