using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker.Converters
{
    /// <summary>
    /// Converts hue [0 - 360] to color.
    /// </summary>
    internal class HueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ColorHelper.FromHSV((double)value, 1, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).GetHue();
        }
    }
}
