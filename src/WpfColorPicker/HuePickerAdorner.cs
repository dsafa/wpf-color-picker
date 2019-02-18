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
    internal class HuePickerAdorner : Adorner
    {
        private static readonly DependencyProperty VerticalPercentProperty =
            DependencyProperty.Register(nameof(VerticalPercent), typeof(double), typeof(HuePickerAdorner), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        private static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(nameof(Color), typeof(Color), typeof(HuePickerAdorner), new FrameworkPropertyMetadata(Colors.Red, FrameworkPropertyMetadataOptions.AffectsRender));
        private static readonly Pen Pen = new Pen(Brushes.Black, 2);
        private Brush _brush = Brushes.Red;

        public HuePickerAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        public double VerticalPercent
        {
            get => (double)GetValue(VerticalPercentProperty);
            set => SetValue(VerticalPercentProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set
            {
                SetValue(ColorProperty, value);
                _brush = new SolidColorBrush(value);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var adornedElementRect = new Rect(AdornedElement.DesiredSize);
            var width = adornedElementRect.Width / 3;
            var y = adornedElementRect.Height * VerticalPercent;
            var x = adornedElementRect.Width - width;

            var triangleGeometry = new StreamGeometry();
            using (var context = triangleGeometry.Open())
            {
                context.BeginFigure(new Point(x, y), true, true);
                context.LineTo(new Point(x + width, y + width / 2), true, false);
                context.LineTo(new Point(x + width, y - width / 2), true, false);
            }

            drawingContext.DrawGeometry(_brush, Pen, triangleGeometry);
            drawingContext.DrawLine(Pen, new Point(x, y), new Point(0, y));
        }
    }
}
