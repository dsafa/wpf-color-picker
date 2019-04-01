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
    internal partial class HuePicker : SliderPicker
    {
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(HuePicker), new PropertyMetadata(0.0, OnHueChanged));

        public HuePicker()
        {
            InitializeComponent();
        }

        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
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

            AdornerVerticalPercent = percent;
            AdornerColor = ColorHelper.FromHSV(hue, 1, 1);
        }

        protected override void OnAdornerPositionChanged(double verticalPercent)
        {
            Color c = hueGradients.GradientStops.GetColorAtOffset(verticalPercent);
            AdornerColor = c;
            Hue = c.GetHue();
        }
    }
}
