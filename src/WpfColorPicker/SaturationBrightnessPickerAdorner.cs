using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfColorPicker
{
    internal class SaturationBrightnessPickerAdorner : Adorner
    {
        private static readonly DependencyProperty PositionProperty
            = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(SaturationBrightnessPickerAdorner), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));
        private static readonly Brush FillBrush = Brushes.Transparent;
        private static readonly Pen InnerRingPen = new Pen(Brushes.White, 2);
        private static readonly Pen OuterRingPen = new Pen(Brushes.Black, 2);

        internal SaturationBrightnessPickerAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        internal Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawEllipse(FillBrush, InnerRingPen, Position, 4, 4);
            drawingContext.DrawEllipse(FillBrush, OuterRingPen, Position, 6, 6);
        }
    }
}
