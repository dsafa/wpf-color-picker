using System;
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
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(0, null, ValueOnCoerce), Validate);
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(0));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(0));
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register(nameof(Step), typeof(int), typeof(ColorNumericInput), new PropertyMetadata(1));
        private static readonly Regex regex = new Regex(@"[0-9]+", RegexOptions.Compiled);
        private double _lastMouseY;
        private bool _isMouseCaptured;

        public ColorNumericInput()
        {
            InitializeComponent();
            scroller.MouseMove += ScrollerOnMouseMove;
            scroller.MouseDown += ScrollerOnMouseDown;
            scroller.MouseUp += ScrollerOnMouseUp;
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

        public int Step
        {
            get => (int)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                IncrementValue();
            }
            else if (e.Delta < 0)
            {
                DecrementValue();
            }
        }

        private static bool Validate(object o)
        {
            return o is int;
        }

        private static object ValueOnCoerce(DependencyObject d, object baseValue)
        {
            var input = (ColorNumericInput)d;
            return Math.Min(Math.Max(input.Min, (int)baseValue), input.Max);
        }

        private void TextBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void ScrollerOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseCaptured = true;
            _lastMouseY = e.GetPosition(scroller).Y;
            Mouse.Capture(scroller);
        }

        private void ScrollerOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseCaptured = false;
            Mouse.Capture(null);
        }

        private void ScrollerOnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || !_isMouseCaptured)
            {
                return;
            }

            var mouseY = e.GetPosition(scroller).Y;
            bool movedUp = mouseY < _lastMouseY;
            bool movedDown = mouseY > _lastMouseY;

            if (movedUp)
            {
                IncrementValue();
            }
            else if (movedDown)
            {
                DecrementValue();
            }

            _lastMouseY = mouseY;
        }

        private void IncrementValue()
        {
            Value = Math.Min(Value + Step, Max);
        }

        private void DecrementValue()
        {
            Value = Math.Max(0, Value - Step);
        }
    }
}
