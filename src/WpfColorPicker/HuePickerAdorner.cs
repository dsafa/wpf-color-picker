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
        private const double TriangleLength = 20;
        private const double X = 5;
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
            var y = adornedElementRect.Height * VerticalPercent;

            var leftTriGeometry = new StreamGeometry();
            using (var geometryContext = leftTriGeometry.Open())
            {
                geometryContext.BeginFigure(new Point(-X, y + TriangleLength / 2), true, true);
                geometryContext.LineTo(new Point(-X + TriangleLength, y), true, false);
                geometryContext.LineTo(new Point(-X, y - TriangleLength / 2), true, false);
            }

            var rightTriGeometry = leftTriGeometry.Clone();
            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new TranslateTransform(-adornedElementRect.Width, 0));
            transformGroup.Children.Add(new ScaleTransform(-1, 1));
            rightTriGeometry.Transform = transformGroup;

            drawingContext.DrawGeometry(_brush, Pen, leftTriGeometry);
            drawingContext.DrawGeometry(_brush, Pen, rightTriGeometry);
        }
    }
}
