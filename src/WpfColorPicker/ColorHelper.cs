using System;
using System.Windows.Media;

namespace WpfColorPicker
{
    internal static class ColorHelper
    {
        /// <summary>
        /// Creates a color from HSV / HSB values.
        /// </summary>
        /// <param name="h">Hue, [0 - 360]</param>
        /// <param name="s">Saturation, [0, 1]</param>
        /// <param name="v">Value, [0, 1]</param>
        /// <returns>The color created from hsv values.</returns>
        /// <remarks>Algorithm from https://en.wikipedia.org/wiki/HSL_and_HSV #From Hsv</remarks>
        internal static Color FromHSV(double h, double s, double v)
        {
            h = Clamp(h, 0, 360);
            s = Clamp(s, 0, 1);
            v = Clamp(v, 0, 1);

            byte f(double n)
            {
                double k = (n + h / 60) % 6;
                double value = v - v * s * Math.Max(Math.Min(Math.Min(k, 4 - k), 1), 0);
                return (byte)(value * 255);
            }

            return Color.FromRgb(f(5), f(3), f(1));
        }

        private static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }
    }
}
