using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfColorPicker
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

        public Color Color
        {
            get => _color;
            set
            {
                if (value == _color)
                {
                    return;
                }

                if (!_dirty)
                {
                    _oldColor = _color;
                    _dirty = true;
                }

                _color = value;
                OnPropertyChanged();

                _red = _color.R;
                _green = _color.G;
                _blue = _color.B;
                _alpha = _color.A;

                OnPropertyChanged(nameof(Red));
                OnPropertyChanged(nameof(Green));
                OnPropertyChanged(nameof(Blue));
                OnPropertyChanged(nameof(Alpha));

                _hue = _color.GetHue();
                _saturation = _color.GetSaturation();
                _brightness = _color.GetBrightness();

                OnPropertyChanged(nameof(Hue));
                OnPropertyChanged(nameof(Saturation));
                OnPropertyChanged(nameof(Brightness));
            }
        }

        public Color OldColor
        {
            get => _oldColor;
            set
            {
                if (value == _oldColor)
                {
                    return;
                }

                _oldColor = value;
                OnPropertyChanged();

                _dirty = false;
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
                OnPropertyChanged(nameof(LateBindHue));
                UpdateRGB();
            }
        }

        public double LateBindHue
        {
            get => _hue;
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
                UpdateRGB();
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
                UpdateRGB();
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
                UpdateHSB();
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
                UpdateHSB();
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
                UpdateHSB();
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
                UpdateHSB();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private void UpdateRGB()
        {
            _color = ColorHelper.FromHSV(Hue, Saturation, Brightness);
            _color = Color.FromArgb(Alpha, _color.R, _color.G, _color.B);
            _red = _color.R;
            _green = _color.G;
            _blue = _color.B;

            OnPropertyChanged(nameof(Red));
            OnPropertyChanged(nameof(Green));
            OnPropertyChanged(nameof(Blue));
            OnPropertyChanged(nameof(Color));
        }

        private void UpdateHSB()
        {
            _color = Color.FromArgb(Alpha, Red, Green, Blue);
            _hue = _color.GetHue();
            _saturation = _color.GetSaturation();
            _brightness = _color.GetBrightness();

            OnPropertyChanged(nameof(Hue));
            OnPropertyChanged(nameof(Saturation));
            OnPropertyChanged(nameof(Brightness));
            OnPropertyChanged(nameof(Color));
        }
    }
}
