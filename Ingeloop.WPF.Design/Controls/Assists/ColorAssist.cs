using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ingeloop.WPF.Design
{
    public static class ColorAssist
    {
        private static readonly Brush DefaultCustomColor1 = Brushes.LightSteelBlue;
        private static readonly Brush DefaultCustomColor2 = Brushes.SteelBlue;

        #region AttachedProperty : CustomColor1Property
        /// <summary>
        /// Define a custom color, index 1, used for Certain Controls
        /// </summary>
        public static readonly DependencyProperty CustomColor1Property
            = DependencyProperty.RegisterAttached("CustomColor1", typeof(Brush), typeof(ColorAssist), new PropertyMetadata(DefaultCustomColor1));

        public static Brush GetCustomColor1(DependencyObject element) => (Brush)element.GetValue(CustomColor1Property);
        public static void SetCustomColor1(DependencyObject element, Brush value) => element.SetValue(CustomColor1Property, value);
        #endregion

        #region AttachedProperty : CustomColor2Property
        /// <summary>
        /// Define a custom color, index 1, used for Certain Controls
        /// </summary>
        public static readonly DependencyProperty CustomColor2Property
            = DependencyProperty.RegisterAttached("CustomColor2", typeof(Brush), typeof(ColorAssist), new PropertyMetadata(DefaultCustomColor2));

        public static Brush GetCustomColor2(DependencyObject element) => (Brush)element.GetValue(CustomColor2Property);
        public static void SetCustomColor2(DependencyObject element, Brush value) => element.SetValue(CustomColor2Property, value);
        #endregion
    }
}
