using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    /// <summary>
    /// Window that contains a <see cref="ColorPicker"/> and a color palette.
    /// </summary>
    public partial class ColorPickerDialog : Window
    {
        private static readonly ObservableCollection<Color> DefaultPalette = new ObservableCollection<Color>(new Color[] {
            Colors.Black, Colors.Gray, Colors.White, Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Blue, Colors.Indigo, Colors.Magenta,
            Colors.Purple, Colors.DarkRed, Colors.DarkOrange, Colors.DarkGreen, Colors.DarkBlue, Colors.DarkMagenta, Colors.MediumPurple,
            Colors.LightGreen, Colors.LightBlue
        });

        /// <summary>
        /// Creates an instance of the <see cref="ColorPickerDialog"/> class
        /// with a default color and default color palette.
        /// </summary>
        public ColorPickerDialog()
            : this(Colors.Red)
        {
        }

        /// <summary>
        /// Creates an instance of the <see cref="ColorPickerDialog"/> class
        /// with an initial <see cref="Color"/> and a default color palette.
        /// </summary>
        /// <param name="color">The initial color.</param>
        public ColorPickerDialog(Color color)
            : this(color, DefaultPalette)
        {
        }

        /// <summary>
        /// Creates an instanace of the <see cref="ColorPickerDialog"/> class
        /// with an initial <see cref="Color"/> and a color palette.
        /// </summary>
        /// <param name="color">The initial color.</param>
        /// <param name="palette">The color palette.</param>
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

        /// <summary>
        /// Gets the selected color.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets the color palette.
        /// </summary>
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
