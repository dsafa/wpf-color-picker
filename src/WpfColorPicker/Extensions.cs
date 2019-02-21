using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    internal static class Extensions
    {
        /// <summary>
        /// Gets the color of a gradient stop collection with the given index
        /// </summary>
        /// <param name="collection">Colletion of colors</param>
        /// <param name="offset">The offset</param>
        /// <returns>The color at the offset</returns>
        internal static Color GetColorAtOffset(this GradientStopCollection collection, double offset)
        {
            GradientStop[] stops = collection.OrderBy(x => x.Offset).ToArray();
            if (offset <= 0)
            {
                return stops[0].Color;
            }
            else if (offset >= 1)
            {
                return stops[stops.Length - 1].Color;
            }

            GradientStop left = stops[0];
            GradientStop right = null;

            foreach (GradientStop stop in stops)
            {
                if (stop.Offset >= offset)
                {
                    right = stop;
                    break;
                }

                left = stop;
            }

            double percent = Math.Round((offset - left.Offset) / (right.Offset - left.Offset), 3);
            byte a = (byte)((right.Color.A - left.Color.A) * percent + left.Color.A);
            byte r = (byte)((right.Color.R - left.Color.R) * percent + left.Color.R);
            byte g = (byte)((right.Color.G - left.Color.G) * percent + left.Color.G);
            byte b = (byte)((right.Color.B - left.Color.B) * percent + left.Color.B);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// Gets hue in hsb model.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The hue of the color from [0, 360]</returns>
        internal static double GetHue(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B).GetHue();
        }

        /// <summary>
        /// Gets the brightness in hsb model.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The brightness of the color from [0, 1]</returns>
        internal static double GetBrightness(this Color color)
        {
            // HSL to HSB conversion
            var c = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            var hslSat = c.GetSaturation();
            var l = c.GetBrightness(); // actaully luminance

            return l + hslSat * Math.Min(l, 1 - l);
        }

        /// <summary>
        /// Gets the saturation in hsb model.
        /// </summary>
        /// <param name="color">The color</param>
        /// <returns>The saturation of the color from [0, 1]</returns>
        internal static double GetSaturation(this Color color)
        {
            // HSL to HSB conversion
            var c = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            var l = c.GetBrightness();
            var b = GetBrightness(color);

            if (b == 0)
            {
                return 0;
            }
            else
            {
                return 2 - (2 * l / b);
            }
        }

        /// <summary>
        /// Clamps the point to the inside of the element
        /// </summary>
        /// <param name="p">The point</param>
        /// <param name="element">The element</param>
        /// <returns>A point inside the element</returns>
        internal static Point Clamp(this Point p, FrameworkElement element)
        {
            var pos = Mouse.GetPosition(element);
            pos.X = Math.Min(Math.Max(0, pos.X), element.ActualWidth);
            pos.Y = Math.Min(Math.Max(0, pos.Y), element.ActualHeight);
            return pos;
        }
    }
}
