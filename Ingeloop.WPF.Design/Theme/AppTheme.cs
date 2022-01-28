using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ingeloop.WPF.Design
{
    public class AppTheme : ResourceDictionary
    {
        private Color primaryColor;
        public Color PrimaryColor
        {
            get
            {
                return primaryColor;
            }
            set
            {
                if (primaryColor != value)
                {
                    primaryColor = value;
                    SetTheme();
                }
            }
        }

        private Color secondaryColor;
        public Color SecondaryColor
        {
            get
            {
                return secondaryColor;
            }
            set
            {
                if (secondaryColor != value)
                {
                    secondaryColor = value;
                    SetTheme();
                }
            }
        }

        private void SetTheme()
        {
            SetSolidColorBrush("Primary", PrimaryColor);
            SetSolidColorBrush("Secondary", SecondaryColor);
        }

        private void SetSolidColorBrush(string colorName, Color colorValue)
        {
            if (colorName is null) throw new ArgumentNullException(nameof(colorName));

            this[colorName] = colorValue;

            string brushName = colorName + "Brush";
            if (this[brushName] is SolidColorBrush brush)
            {
                if (brush.Color == colorValue) return;
            }

            var newBrush = new SolidColorBrush(colorValue);
            newBrush.Freeze();
            this[brushName] = newBrush;
        }
    }
}
