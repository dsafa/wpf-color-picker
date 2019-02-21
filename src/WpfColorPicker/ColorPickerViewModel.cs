using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    internal class ColorPickerViewModel : INotifyPropertyChanged
    {
        private Color _color = Colors.Red;
        private Color _oldColor = Colors.Red;
        private double _hue;
        private double _saturation = 1;
        private double _brightness = 1;
        private byte _red;
        private byte _green;
        private byte _blue;
        private byte _alpha = 255;
        private bool _dirty;
        // flags to prevent circular updates
        private bool _updateFromColor = false;
        private bool _updateFromComponents = false;

        public Color Color
        {
            get => _color;
            set
            {
                if (value == _color)
                {
                    return;
                }

                _color = value;
                OnPropertyChanged();

                if (!_updateFromComponents)
                {
                    UpdateComponents();
                }
            }
        }

        public Color OldColor
        {
            get => _oldColor;
            set
            {
                if (value == _oldColor || _dirty)
                {
                    return;
                }

                _oldColor = value;
                OnPropertyChanged();
                _dirty = true;
            }
        }

        public double Hue
        {
            get => _hue;
            set
            {
                if (value == _hue)
                {
                    return;
                }

                _hue = value;
                OnPropertyChanged();

                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromHSB();
                }
            }
        }

        public double Saturation
        {
            get => _saturation;
            set
            {
                if (value == _saturation)
                {
                    return;
                }

                _saturation = value;
                OnPropertyChanged();

                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromHSB();
                }
            }
        }

        public double Brightness
        {
            get => _brightness;
            set
            {
                if (value == _brightness)
                {
                    return;
                }

                _brightness = value;
                OnPropertyChanged();

                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromHSB();
                }
            }
        }

        public byte Red
        {
            get => _red;
            set
            {
                if (value == _red)
                {
                    return;
                }

                _red = value;
                OnPropertyChanged();

                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromRGB();
                }
            }
        }

        public byte Green
        {
            get => _green;
            set
            {
                if (value == _green)
                {
                    return;
                }

                _green = value;
                OnPropertyChanged();

                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromRGB();
                }
            }
        }

        public byte Blue
        {
            get => _blue;
            set
            {
                if (value == _blue)
                {
                    return;
                }

                _blue = value;
                OnPropertyChanged();

                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromRGB();
                }
            }
        }

        public byte Alpha
        {
            get => _alpha;
            set
            {
                if (value == _alpha)
                {
                    return;
                }

                _alpha = value;
                OnPropertyChanged();
                
                if (!_updateFromColor && !_updateFromComponents)
                {
                    UpdateColorFromRGB();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private void UpdateColorFromRGB()
        {
            // when rgb changes, update hsb
            _updateFromComponents = true;
            Color = Color.FromArgb(Alpha, Red, Green, Blue);
            Hue = Color.GetHue();
            Saturation = Color.GetSaturation();
            Brightness = Color.GetBrightness();
            _updateFromComponents = false;
        }

        private void UpdateColorFromHSB()
        {
            // when hsb changes, update rgb
            _updateFromComponents = true;
            var c = ColorHelper.FromHSV(Hue, Saturation, Brightness);
            c.A = Alpha;

            Color = c;
            Red = Color.R;
            Green = Color.G;
            Blue = Color.B;
            _updateFromComponents = false;
        }

        private void UpdateComponents()
        {
            // when color changes, update hsb and rgb
            _updateFromColor = true;

            Red = Color.R;
            Green = Color.G;
            Blue = Color.B;
            Alpha = Color.A;

            Hue = Color.GetHue();
            Saturation = Color.GetSaturation();
            Brightness = Color.GetBrightness();

            _updateFromColor = false;
        }
    }
}
