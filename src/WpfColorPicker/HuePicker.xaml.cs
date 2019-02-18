using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfColorPicker
{
    /// <summary>
    /// Interaction logic for HuePicker.xaml
    /// </summary>
    public partial class HuePicker : UserControl
    {
        private static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(HuePicker));
        private readonly HuePickerAdorner _adorner;

        public HuePicker()
        {
            InitializeComponent();
            _adorner = new HuePickerAdorner(hueRectangle);
            Loaded += HuePickerOnLoaded;
        }

        private void HuePickerOnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(this).Add(_adorner);
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            UpdateAdorner(e.GetPosition(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            UpdateAdorner(e.GetPosition(this));
        }

        private void UpdateAdorner(Point mousePos)
        {
            _adorner.VerticalPercent = mousePos.Y / ActualHeight;
        }
    }
}
