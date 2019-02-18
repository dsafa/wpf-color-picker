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
        private static readonly DependencyProperty SelectedColorProperty 
            = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(SaturationBrightnessPicker), new PropertyMetadata(Colors.Red, OnSelectedColorChanged));

        public SaturationBrightnessPicker()
        {
            InitializeComponent();
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = (SaturationBrightnessPicker)o;
            picker.hue.Color = (Color)e.NewValue;
        }
    }
}
