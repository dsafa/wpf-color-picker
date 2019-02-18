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
        private static readonly Pen Pen = new Pen(Brushes.Black, 2);
        private static readonly Brush Brush = Brushes.Transparent;

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

            drawingContext.DrawEllipse(Brush, Pen, Position, 6, 6);
        }
    }
}
