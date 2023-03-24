using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ingeloop.WPF.Design
{
    public class MailBoxTabControl : TabControl
    {
        public static readonly DependencyProperty SidePanelWidthProperty = DependencyProperty.Register(nameof(SidePanelWidth), typeof(GridLength), typeof(MailBoxTabControl),
            new PropertyMetadata(new GridLength(120)));

        public static readonly DependencyProperty TopControlProperty = DependencyProperty.Register(nameof(TopControl), typeof(UIElement), typeof(MailBoxTabControl),
            new PropertyMetadata(null));

        public GridLength SidePanelWidth
        {
            get { return (GridLength)GetValue(SidePanelWidthProperty); }
            set { SetValue(SidePanelWidthProperty, value); }
        }

        public UIElement TopControl
        {
            get { return (UIElement)GetValue(TopControlProperty); }
            set { SetValue(TopControlProperty, value); }
        }

        public MailBoxTabControl()
        {
        }
    }
}
