using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            : this(Colors.Red)
        {
        }

        public ColorPickerDialog(Color color)
            : this(color, DefaultPalette)
        {
        }

        public ColorPickerDialog(Color color, IEnumerable<Color> palette)
        {
            if (palette == null)
            {
                throw new ArgumentNullException(nameof(palette));
            }

            Palette = new ObservableCollection<Color>(palette);
            InitializeComponent();
            colorPicker.Color = color;
        }

        public Color Color { get; private set; }

        public ObservableCollection<Color> Palette { get; }

        private void PaletteButtonOnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var color = ((SolidColorBrush)button.Background).Color;
            colorPicker.Color = color;
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Color = colorPicker.Color;
            Close();
        }
    }
}
