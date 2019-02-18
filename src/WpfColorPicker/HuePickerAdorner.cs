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
    class HuePickerAdorner : Adorner
    {
        private static readonly DependencyProperty VerticalPercentProperty =
            DependencyProperty.Register(nameof(VerticalPercent), typeof(double), typeof(HuePickerAdorner), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        private const double TriangleLength = 20;

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

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var adornedElementRect = new Rect(AdornedElement.DesiredSize);
            var x = -5;
            var y = adornedElementRect.Height * VerticalPercent;

            var arrowColor = Brushes.White;
            var pen = new Pen(Brushes.Black, 2);

            var triangleGeometry = new StreamGeometry();
            using (var geometryContext = triangleGeometry.Open())
            {
                geometryContext.BeginFigure(new Point(x, y + TriangleLength / 2), true, true);
                geometryContext.LineTo(new Point(x + TriangleLength, y), true, false);
                geometryContext.LineTo(new Point(x, y - TriangleLength / 2), true, false);
            }

            drawingContext.DrawGeometry(arrowColor, pen, triangleGeometry);
        }
    }
}
