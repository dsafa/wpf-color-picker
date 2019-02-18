using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty ColorProperty
            = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Red));
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(ColorPicker), new PropertyMetadata(0.0, OnHueChanged));

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

        public ColorPicker()
        {
            InitializeComponent();
        }

        private static void OnHueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)o;
            colorPicker.Color = ColorHelper.FromHSV((double)e.NewValue, 1, 1);
        }
    }
}
