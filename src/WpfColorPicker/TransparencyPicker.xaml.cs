using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    /// <summary>
    /// Interaction logic for TransparencyPicker.xaml
    /// </summary>
    internal partial class TransparencyPicker
    {
        public static readonly DependencyProperty ColorProperty 
            = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(TransparencyPicker), new FrameworkPropertyMetadata(Colors.Red));

        public static readonly DependencyProperty AlphaProperty
            = DependencyProperty.Register(nameof(Alpha), typeof(byte), typeof(TransparencyPicker), new PropertyMetadata((byte)0, OnAlphaChanged));

        public TransparencyPicker()
        {
            InitializeComponent();
            AdornerColor = Colors.Black;
        }

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public byte Alpha
        {
            get => (byte) GetValue(AlphaProperty);
            set => SetValue(AlphaProperty, value);
        }

        private static void OnAlphaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (TransparencyPicker)d;
            picker.AdornerVerticalPercent = (byte)e.NewValue / (double)255;
        }

        protected override void OnAdornerPositionChanged(double verticalPercent)
        {
            Alpha = (byte)(verticalPercent * 255);
        }
    }
}
