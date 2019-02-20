using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for NumericInput.xaml
    /// </summary>
    internal partial class ColorNumericInput : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(0, OnValueChanged));
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(0));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(0));
        private static readonly Regex regex = new Regex(@"[0-9]+", RegexOptions.Compiled);

        public ColorNumericInput()
        {
            InitializeComponent();
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public int Min
        {
            get => (int)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        public int Max
        {
            get => (int)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        private void TextBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void ClampValue()
        {
            if (Value < Min)
            {
                Value = Min;
            }
            else if (Value > Max)
            {
                Value = Max;
            }
        }

        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var input = (ColorNumericInput)o;
            input.ClampValue();
        }
    }
}
