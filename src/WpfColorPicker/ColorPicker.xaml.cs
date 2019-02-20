using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Red, OnColorChanged));

        public ColorPicker()
        {
            InitializeComponent();
            var vm = new ColorPickerViewModel();
            vm.PropertyChanged += ViewModelOnPropertyChanged;
            DataContext = vm;
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        private static void OnColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)o;
            ((ColorPickerViewModel)colorPicker.DataContext).Color = (Color)e.NewValue;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ColorPickerViewModel.Color))
            {
                return;
            }

            Color = ((ColorPickerViewModel)sender).Color;
        }
    }
}
