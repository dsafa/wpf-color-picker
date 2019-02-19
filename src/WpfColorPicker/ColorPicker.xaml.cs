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
            = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Red, OnColorChanged));
        public static readonly DependencyProperty OldColorProperty
            = DependencyProperty.Register(nameof(OldColor), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Red));
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(ColorPicker), new PropertyMetadata(0.0, OnHueChanged));

        private bool _dirty;

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        private Color OldColor
        {
            get => (Color)GetValue(OldColorProperty);
            set => SetValue(OldColorProperty, value);
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
            colorPicker.Hue = (double)e.NewValue;
        }

        private static void OnColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)o;

            if (colorPicker._dirty)
            {
                return;
            }
            else
            {
                colorPicker.OldColor = (Color)e.OldValue;
                colorPicker._dirty = true;
            }
        }

        private void Label_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }
    }
}
