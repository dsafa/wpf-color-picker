using Dsafa.WpfColorPicker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xunit;

namespace WpfColorPickerTests
{
    public class ColorExtensionsTests
    {
        [Fact]
        public void HSBandRgbValuesAreEqual()
        {
            var rgb = Color.FromRgb(255, 255, 0);
            var hsb = ColorHelper.FromHSV(60.0, 1.0, 1.0);

            Assert.Equal(rgb, hsb);
        }

        [Theory]
        [MemberData(nameof(GetColors))]
        public void CanConvertHSBAndBack(Color c)
        {
            var h = c.GetHue();
            var s = c.GetSaturation();
            var b = c.GetBrightness();

            var newcolor = ColorHelper.FromHSV(h, s, b);
            Assert.Equal(h, newcolor.GetHue(), 2);
            Assert.Equal(s, newcolor.GetSaturation(), 2);
            Assert.Equal(b, newcolor.GetBrightness(), 2);
        }

        // split so memory doesnt run out
        public static IEnumerable<object[]> GetColors()
        {
            return typeof(Colors)
                .GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                .Select(p => new object[] { p.GetValue(null) });
        }
    }
}
