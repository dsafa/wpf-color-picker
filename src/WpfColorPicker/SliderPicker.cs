using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    internal abstract class SliderPicker : UserControl
    {
        private readonly SliderPickerAdorner _adorner;

        protected SliderPicker()
        {
            _adorner = new SliderPickerAdorner(this);
            Loaded += PickerOnLoaded;
        }

        protected Color AdornerColor
        {
            get => _adorner.Color;
            set => _adorner.Color = value;
        }

        protected double AdornerVerticalPercent
        {
            get => _adorner.VerticalPercent;
            set => _adorner.VerticalPercent = value;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            Mouse.Capture(this);

            UpdateAdorner(e.GetPosition(this).Clamp(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            Mouse.Capture(null);
            UpdateAdorner(e.GetPosition(this).Clamp(this));
        }

        protected virtual void OnAdornerPositionChanged(double verticalPercent)
        {
        }

        private void UpdateAdorner(Point mousePos)
        {
            double verticalPercent = mousePos.Y / ActualHeight;
            _adorner.VerticalPercent = verticalPercent;
            OnAdornerPositionChanged(verticalPercent);
        }

        private void PickerOnLoaded(object sender, RoutedEventArgs e)
        {
            _adorner.ElementSize = new Rect(new Size(ActualWidth, ActualHeight));
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
        }
    }
}