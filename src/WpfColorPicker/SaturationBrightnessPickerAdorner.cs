using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfColorPicker
{
    internal class SaturationBrightnessPickerAdorner : Adorner
    {
        private static readonly DependencyProperty PositionProperty
            = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(SaturationBrightnessPickerAdorner), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));
        private static readonly DependencyProperty ColorProperty
            = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(SaturationBrightnessPickerAdorner), new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.AffectsRender));
        private static readonly Brush Brush = Brushes.Transparent;
        private static readonly GradientStopCollection Gradients = new GradientStopCollection(new GradientStop[] {
            new GradientStop(Colors.Black, 0), new GradientStop(Colors.Black, 0.4), new GradientStop(Colors.White, 0.7), new GradientStop(Colors.White, 1)
        });


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

        internal Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var elementRect = new Rect(AdornedElement.DesiredSize);
            var verticalPercent = Position.Y / elementRect.Height;

            var pen = new Pen(new SolidColorBrush(Gradients.GetColorAtOffset(verticalPercent)), 2);
            drawingContext.DrawEllipse(Brush, pen, Position, 6, 6);
        }
    }
}
