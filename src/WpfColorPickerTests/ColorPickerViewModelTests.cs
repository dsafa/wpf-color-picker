using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dsafa.WpfColorPicker;
using System.Windows.Media;


namespace WpfColorPickerTests
{
    public class ColorPickerViewModelTests
    {
        [Fact]
        public void UpdatingColorWillAlsoUpdateComponents()
        {
            var vm = new ColorPickerViewModel();
            var color  = Color.FromRgb(0, 170, 255); // #00aaff hsb: 200, 100%, 100%

            vm.Color = color;

            Assert.Equal(color, vm.Color);
            Assert.Equal(color.R, vm.Red);
            Assert.Equal(color.G, vm.Green);
            Assert.Equal(color.B, vm.Blue);
            Assert.Equal(200.0, vm.Hue, 2);
            Assert.Equal(1.0, vm.Saturation, 2);
            Assert.Equal(1.0, vm.Brightness, 2);
        }

        [Fact]
        public void UpdatingRGBWillAlsoUpdateHSBAndColor()
        {
            var vm = new ColorPickerViewModel();
            // #00c896 hsb: 165, 100%, 78.43%
            const byte red = 0;
            const byte green = 200;
            const byte blue = 150;
            const double hue = 165.0;
            const double sat = 1.0;
            const double bright = 0.78;
            vm.Red = red;
            vm.Green = green;
            vm.Blue = blue;

            Assert.Equal(red, vm.Red);
            Assert.Equal(green, vm.Green);
            Assert.Equal(blue, vm.Blue);
            Assert.Equal(Color.FromRgb(red, green, blue), vm.Color);
            Assert.Equal(hue, vm.Hue, 2);
            Assert.Equal(sat, vm.Saturation, 2);
            Assert.Equal(bright, vm.Brightness, 2);
        }

        [Fact]
        public void UpdatingHSBWillAlsoUpdateRGBAndColor()
        {
            var vm = new ColorPickerViewModel();
            // #6a4164 hsb: 308.78, 38.68%, 41.57%
            const byte red = 106;
            const byte green = 65;
            const byte blue = 100;
            const double hue = 308.78;
            const double sat = 0.3868;
            const double bright = 0.4157;
            vm.Hue = hue;
            vm.Saturation = sat;
            vm.Brightness = bright;

            Assert.Equal(hue, vm.Hue, 2);
            Assert.Equal(sat, vm.Saturation, 2);
            Assert.Equal(bright, vm.Brightness, 2);
            Assert.Equal(ColorHelper.FromHSV(hue, sat, bright), vm.Color);
            Assert.Equal(red, vm.Red);
            Assert.Equal(green, vm.Green);
            Assert.Equal(blue, vm.Blue);
        }
    }
}
