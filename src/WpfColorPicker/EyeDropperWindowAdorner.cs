using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Dsafa.WpfColorPicker
{
    internal class EyeDropperWindowAdorner : Adorner
    {
        public static readonly DependencyProperty PositionProperty
            = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(EyeDropperWindowAdorner), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));
        private const int AdornerWidth = 120;
        private const int AdornerHeight = 200;
        private static readonly Pen StrokePen = new Pen(Brushes.Black, 2);
        private static readonly Typeface Typeface = new Typeface(new FontFamily("Consolas"), FontStyles.Normal, FontWeights.Heavy, FontStretches.Normal);

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
            var elementRect = new Rect(AdornedElement.DesiredSize);
            var zoomDiameter = AdornerWidth;
            var radius = zoomDiameter / 2;

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new TranslateTransform(Position.X, Position.Y));

            if (Position.X > elementRect.Width - AdornerWidth)
            {
                transformGroup.Children.Add(new TranslateTransform(-AdornerWidth, 0));
            }

            if (Position.Y > elementRect.Height - AdornerHeight)
            {
                transformGroup.Children.Add(new TranslateTransform(0, -AdornerHeight));
            }

            drawingContext.PushTransform(transformGroup);

            // zoom view
            drawingContext.DrawEllipse(Brushes.White, StrokePen, new Point(radius, radius), radius, radius);
            drawingContext.PushClip(new EllipseGeometry(new Point(radius, radius), radius, radius));
            drawingContext.DrawImage(SurroundingPixels, new Rect(0, 0, zoomDiameter, zoomDiameter));
            var crosshair = new Pen(Brushes.Black, 2);
            var crosshairTop = new Point(radius, 0);
            var crosshairBot = new Point(radius, zoomDiameter);
            var crosshairLeft = new Point(0, radius);
            var crosshairRight = new Point(zoomDiameter, radius);
            drawingContext.DrawLine(crosshair, crosshairTop, crosshairBot);
            drawingContext.DrawLine(crosshair, crosshairLeft, crosshairRight);
            drawingContext.Pop();

            // color display
            var text = new FormattedText($"#{Color.R:X2}{Color.G:X2}{Color.B:X2}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface, 24, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
            var displayX = (AdornerWidth - text.Width) / 2;
            var displayY = zoomDiameter + 10;
            drawingContext.DrawRoundedRectangle(new SolidColorBrush(Color), StrokePen, new Rect(0, displayY, AdornerWidth, text.Height * 1.5), 2, 2);
            var textGeometry = text.BuildGeometry(new Point(displayX, displayY + 10));
            drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.White, 1), textGeometry);

            drawingContext.Pop();
        }
    }
}
