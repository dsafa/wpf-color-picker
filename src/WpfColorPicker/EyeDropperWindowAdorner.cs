using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfColorPicker
{
    internal class EyeDropperWindowAdorner : Adorner
    {
        public static readonly DependencyProperty PositionProperty
            = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(EyeDropperWindowAdorner), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));
        private const int ImageWidth = 50;

        public EyeDropperWindowAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        public Color Color { get; set; }
        public ImageSource SurroundingPixels { get; set; }
        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawImage(SurroundingPixels, new Rect(Position.X, Position.Y, ImageWidth, ImageWidth));
            var crosshair = new Pen(Brushes.Black, 2);
            var crosshairTop = new Point(Position.X + ImageWidth / 2, Position.Y);
            var crosshairBot = new Point(Position.X + ImageWidth / 2, Position.Y + ImageWidth);
            var crosshairLeft = new Point(Position.X, Position.Y + ImageWidth / 2);
            var crosshairRight = new Point(Position.X + ImageWidth, Position.Y + ImageWidth / 2);
            drawingContext.DrawLine(crosshair, crosshairTop, crosshairBot);
            drawingContext.DrawLine(crosshair, crosshairLeft, crosshairRight);

            drawingContext.DrawRectangle(new SolidColorBrush(Color), null, new Rect(Position.X + ImageWidth, Position.Y, 50, 20));
        }
    }
}
