using Ingeloop.WPF.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ingeloop.WPF.Design
{
    public class MailBoxTabItem : TabItem
    {
        public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register(nameof(IconKind), typeof(PackIconKind), typeof(MailBoxTabItem),
            new PropertyMetadata(PackIconKind.PlusBold));

        public static readonly DependencyProperty SideTextProperty = DependencyProperty.Register(nameof(SideText), typeof(string), typeof(MailBoxTabItem),
            new PropertyMetadata(""));

        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        public string SideText
        {
            get { return (string)GetValue(SideTextProperty); }
            set { SetValue(SideTextProperty, value); }
        }

        public MailBoxTabItem()
        {
        }
    }
}
