using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfColorPicker
{
    internal static class Extensions
    {
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
        /// <param name="color"></param>
        /// <returns></returns>
        internal static double GetHue(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B).GetHue();
        }

        /// <summary>
        /// Gets the brightness in hsb model.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static double GetBrightness(this Color color)
        {
            var c = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            var hslSat = c.GetSaturation();
            var l = c.GetBrightness(); // actaully luminance

            return l + hslSat * Math.Min(l, 1 - l);
        }

        internal static double GetSaturation(this Color color)
        {
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
    }
}
