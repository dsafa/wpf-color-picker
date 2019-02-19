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
        public static readonly DependencyProperty HueProperty = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(SaturationBrightnessPicker), new PropertyMetadata(0.0, OnHueChanged));
        public static readonly DependencyProperty SaturationProperty = DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(SaturationBrightnessPicker), new PropertyMetadata(1.0, OnSaturationChanged));
        public static readonly DependencyProperty BrightnessProperty = DependencyProperty.Register(nameof(Brightness), typeof(double), typeof(SaturationBrightnessPicker), new PropertyMetadata(1.0, OnBrightnessChanged));
        private readonly SaturationBrightnessPickerAdorner _adorner;

        public SaturationBrightnessPicker()
        {
            InitializeComponent();
            _adorner = new SaturationBrightnessPickerAdorner(this);
            Loaded += SaturationBrightnessPickerOnLoaded;
        }

        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public double Saturation
        {
            get => (double)GetValue(SaturationProperty);
            set => SetValue(SaturationProperty, value);
        }

        public double Brightness
        {
            get => (double)GetValue(BrightnessProperty);
            set => SetValue(BrightnessProperty, value);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            UpdateAdorner(e.GetPosition(this));
            UpdateColor(e.GetPosition(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            UpdateAdorner(e.GetPosition(this));
            UpdateColor(e.GetPosition(this));
        }

        private static void OnHueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (SaturationBrightnessPicker)o;
            picker.UpdateColor(picker._adorner.Position);
        }

        private static void OnSaturationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (SaturationBrightnessPicker)o;
            var x = picker.Saturation * picker.ActualWidth;
            picker.UpdateAdorner(new Point(x, picker._adorner.Position.Y));
        }

        private static void OnBrightnessChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (SaturationBrightnessPicker)o;
            var y = (1 - picker.Brightness) * picker.ActualWidth;
            picker.UpdateAdorner(new Point(picker._adorner.Position.X, y));
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
        }

        private void UpdateColor(Point p)
        {
            Saturation = p.X / ActualWidth;
            Brightness = 1 - (p.Y / ActualHeight); // directions reversed
        }
    }
}
