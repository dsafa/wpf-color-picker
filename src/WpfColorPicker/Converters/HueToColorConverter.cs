using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfColorPicker.Converters
{
    internal class HueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Color))
            {
                throw new ArgumentException("Requires type of Color", nameof(targetType));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(value is double hue))
            {
                throw new ArgumentException("Value is not type double", nameof(value));
            }

            return ColorHelper.FromHSV(hue, 1, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (targetType != typeof(double))
            {
                throw new ArgumentException("Expected type double", nameof(targetType));
            }

            if (!(value is Color c))
            {
                throw new ArgumentException("Expected type color", nameof(value));
            }

            return c.GetHue();
        }
    }
}
