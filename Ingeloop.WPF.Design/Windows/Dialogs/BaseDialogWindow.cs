using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ingeloop.WPF.Design
{
    public class BaseDialogWindow : Window
    {
        public BaseDialogWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowInTaskbar = false;

            Loaded += (o, e) =>
            {
                ((Button)Template.FindName("CloseButton", this)).Click += CloseButton_Click;
                ((Grid)Template.FindName("HeaderGrid", this)).MouseDown += WindowHeader_MouseDown;
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
