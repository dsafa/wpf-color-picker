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
        private static readonly DependencyProperty SelectedColorProperty
            = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(HuePicker), new PropertyMetadata(Colors.Red, OnSelectedColorChanged));
        private readonly HuePickerAdorner _adorner;

        public HuePicker()
        {
            InitializeComponent();
            _adorner = new HuePickerAdorner(hueRectangle);
            Loaded += HuePickerOnLoaded;
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

        private static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var huePicker = (HuePicker)o;
            huePicker.UpdateAdorner((Color)e.NewValue);
        }

        private void UpdateAdorner(Color color)
        {
            double hue = color.GetHue();
            double percent = hue / 360;

            // Make it so that the arrow doesn't jump back to the top when it goes to the bottom
            Point mousePos = Mouse.GetPosition(this);
            if (percent == 0 && ActualHeight - mousePos.Y < 1)
            {
                percent = 1;
            }

            _adorner.VerticalPercent = percent;
            _adorner.Color = color;
        }

        private void UpdateAdorner(Point mousePos)
        {
            double verticalPercent = mousePos.Y / ActualHeight;
            _adorner.VerticalPercent = verticalPercent;

            Color c = hueGradients.GradientStops.GetColorAtOffset(verticalPercent);
            _adorner.Color = c;
            SelectedColor = c;
        }

        private void HuePickerOnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
        }
    }
}
