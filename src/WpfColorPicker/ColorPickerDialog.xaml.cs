using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for ColorPickerDialog.xaml
    /// </summary>
    public partial class ColorPickerDialog : Window
    {
        private static readonly ObservableCollection<Color> DefaultPalette = new ObservableCollection<Color>(new Color[] {
            Colors.Black, Colors.Gray, Colors.White, Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Blue, Colors.Indigo, Colors.Magenta,
            Colors.Purple, Colors.DarkRed, Colors.DarkOrange, Colors.DarkGreen, Colors.DarkBlue, Colors.DarkMagenta, Colors.MediumPurple,
            Colors.LightGreen, Colors.LightBlue
        });

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        public ObservableCollection<Color> Palette { get; } = new ObservableCollection<Color>(DefaultPalette);

        private void PaletteButtonOnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var color = ((SolidColorBrush)button.Background).Color;
            colorPicker.Color = color;
        }
    }
}
