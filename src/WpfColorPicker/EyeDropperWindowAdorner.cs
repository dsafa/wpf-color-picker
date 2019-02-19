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

        public EyeDropperWindowAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        public Color Color { get; set; }
        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(new SolidColorBrush(Color), null, new Rect(Position.X, Position.Y, 100, 50));
        }
    }
}
