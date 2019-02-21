using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    /// <summary>
    /// Interaction logic for HuePicker.xaml
    /// </summary>
    internal partial class HuePicker : UserControl
    {
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(HuePicker), new PropertyMetadata(0.0, OnHueChanged));
        private readonly HuePickerAdorner _adorner;

        public HuePicker()
        {
            InitializeComponent();
            _adorner = new HuePickerAdorner(hueRectangle);
            Loaded += HuePickerOnLoaded;
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

            Mouse.Capture(this);

            UpdateAdorner(e.GetPosition(this).Clamp(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            Mouse.Capture(null);
            UpdateAdorner(e.GetPosition(this).Clamp(this));
        }

        private static void OnHueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var huePicker = (HuePicker)o;
            huePicker.UpdateAdorner((double)e.NewValue);
        }

        private void UpdateAdorner(double hue)
        {
            double percent = hue / 360;

            // Make it so that the arrow doesn't jump back to the top when it goes to the bottom
            Point mousePos = Mouse.GetPosition(this);
            if (percent == 0 && ActualHeight - mousePos.Y < 1)
            {
                percent = 1;
            }

            _adorner.VerticalPercent = percent;
            _adorner.Color = ColorHelper.FromHSV(hue, 1, 1);
        }

        private void UpdateAdorner(Point mousePos)
        {
            double verticalPercent = mousePos.Y / ActualHeight;
            _adorner.VerticalPercent = verticalPercent;

            Color c = hueGradients.GradientStops.GetColorAtOffset(verticalPercent);
            _adorner.Color = c;
            Hue = c.GetHue();
        }

        private void HuePickerOnLoaded(object sender, RoutedEventArgs e)
        {
            _adorner.ElementSize = new Rect(new Size(hueRectangle.ActualWidth, hueRectangle.ActualHeight));
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
        }
    }
}
