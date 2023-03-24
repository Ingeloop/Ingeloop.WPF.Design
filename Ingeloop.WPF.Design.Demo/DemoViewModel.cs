using Ingeloop.WPF.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeloop.WPF.Design.Demo
{
    public class DemoViewModel : BaseViewModel
    {
        public RelayCommand MessageDialogCommand => new RelayCommand(MessageDialog);
        public RelayCommand ConfirmDialogCommand => new RelayCommand(ConfirmDialog);
        public RelayCommand InputDialogCommand => new RelayCommand(InputDialog);

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public DemoViewModel()
        {
            Products.Add(new Product { ID = "1", Name = "Product 1", Description = "Description 1", Quantity = 5 });
            Products.Add(new Product { ID = "2", Name = "Product 2", Description = "Description 2", Quantity = 2 });
            Products.Add(new Product { ID = "3", Name = "Product 3", Description = "Description 3", Quantity = 8 });
            Products.Add(new Product { ID = "4", Name = "Product 4", Description = "Description 4", Quantity = 12 });
            Products.Add(new Product { ID = "5", Name = "Product 5", Description = "Description 5", Quantity = 3 });
        }

        private void MessageDialog()
        {
            MessageDialogViewModel messageDialogViewModel = new MessageDialogViewModel {Title = "Message",  Message = "Hello !", OkText = "OK" };
            messageDialogViewModel.ShowDialog();
        }

        private void ConfirmDialog()
        {
            ConfirmDialogViewModel confirmDialogViewModel = new ConfirmDialogViewModel("Confirm", "Please confirm !");
            bool? result = confirmDialogViewModel.ShowDialog();
        }

        private void InputDialog()
        {
            TextInputDialogViewModel textInputDialogViewModel = new TextInputDialogViewModel(false) { Title = "Input", Message = "Please enter a value !", OkText = "Send" };
            if (textInputDialogViewModel.ShowDialog() == true)
            {
                string result = textInputDialogViewModel.Text;
            }
        }
    }

    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public Product()
        {
        }
    }
}
