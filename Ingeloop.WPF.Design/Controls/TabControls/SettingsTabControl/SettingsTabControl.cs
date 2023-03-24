using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ingeloop.WPF.Design
{
    public class SettingsTabControl : TabControl
    {
        public static readonly DependencyProperty SidePanelWidthProperty = DependencyProperty.Register(nameof(SidePanelWidth), typeof(GridLength), typeof(SettingsTabControl),
            new PropertyMetadata(new GridLength(120)));

        public GridLength SidePanelWidth
        {
            get { return (GridLength)GetValue(SidePanelWidthProperty); }
            set { SetValue(SidePanelWidthProperty, value); }
        }

        public SettingsTabControl()
        {
        }
    }
}
