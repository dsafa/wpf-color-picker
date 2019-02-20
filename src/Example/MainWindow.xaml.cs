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
using WpfColorPicker;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DependencyProperty ColorProperty
            = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(MainWindow), new PropertyMetadata(Colors.Black));

        public MainWindow()
        {
            InitializeComponent();
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorPickerDialog(Color);
            var res = dialog.ShowDialog();
            if (res.HasValue && res.Value)
            {
                Color = dialog.Color;
            }
        }
    }
}
