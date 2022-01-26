using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ingeloop.WPF.Design
{
    public partial class CustomButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(CustomButton), new PropertyMetadata((double)5));
        public static readonly DependencyProperty HoverBrushProperty = DependencyProperty.Register(nameof(HoverBrush), typeof(Brush), typeof(CustomButton), new PropertyMetadata((Brush)null));
        public static readonly DependencyProperty HoverOpacityProperty = DependencyProperty.Register(nameof(HoverOpacity), typeof(double), typeof(CustomButton), new PropertyMetadata((double)1));

        public CustomButton()
        {
        }

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Brush HoverBrush
        {
            get { return (Brush)GetValue(HoverBrushProperty); }
            set { SetValue(HoverBrushProperty, value); }
        }

        public double HoverOpacity
        {
            get { return (double)GetValue(HoverOpacityProperty); }
            set { SetValue(HoverOpacityProperty, value); }
        }
    }
}
