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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfColorPicker
{
    public partial class SaturationBrightnessPicker : UserControl
    {
        public static readonly DependencyProperty ColorProperty 
            = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(SaturationBrightnessPicker), new PropertyMetadata(Colors.Red, OnColorChanged));
        public static readonly DependencyProperty HueProperty 
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(SaturationBrightnessPicker), new PropertyMetadata(0.0, OnHueChanged));
        private readonly SaturationBrightnessPickerAdorner _adorner;

        public SaturationBrightnessPicker()
        {
            InitializeComponent();
            _adorner = new SaturationBrightnessPickerAdorner(this);
            Loaded += SaturationBrightnessPickerOnLoaded;
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
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

        private static void OnColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (SaturationBrightnessPicker)o;
        }

        private static void OnHueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (SaturationBrightnessPicker)o;
            picker.UpdateColor(Mouse.GetPosition(picker));
        }

        private void SaturationBrightnessPickerOnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
            _adorner.IsClipEnabled = true;
            _adorner.Position = new Point(ActualWidth, 0);
        }

        private void UpdateAdorner(Point p)
        {
            _adorner.Position = p;
            UpdateColor(p);
        }

        private void UpdateColor(Point p)
        {
            var saturationPercent = p.X / ActualWidth;
            var brightnessPercent = 1 - (p.Y / ActualHeight); // directions reversed

            Color = ColorHelper.FromHSV(Hue, saturationPercent, brightnessPercent);
        }
    }
}
