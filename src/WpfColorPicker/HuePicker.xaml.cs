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
        public static readonly DependencyProperty SelectedHueProperty = DependencyProperty.Register(nameof(SelectedHue), typeof(double), typeof(HuePicker), new PropertyMetadata(0.0, OnSelectedHueChanged));
        private readonly HuePickerAdorner _adorner;

        public HuePicker()
        {
            InitializeComponent();
            _adorner = new HuePickerAdorner(hueRectangle);
            Loaded += HuePickerOnLoaded;
        }

        public double SelectedHue
        {
            get => (double)GetValue(SelectedHueProperty);
            set => SetValue(SelectedHueProperty, value);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            Mouse.Capture(this);

            UpdateAdorner(e.GetPosition(this).Clip(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            Mouse.Capture(null);
            UpdateAdorner(e.GetPosition(this).Clip(this));
        }

        private static void OnSelectedHueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
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
            SelectedHue = c.GetHue();
        }

        private void HuePickerOnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
        }
    }
}
