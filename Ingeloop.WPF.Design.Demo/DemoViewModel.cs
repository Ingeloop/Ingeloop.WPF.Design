using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeloop.WPF.Design.Demo
{
    public class DemoViewModel
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public DemoViewModel()
        {
            Products.Add(new Product { ID = "1", Name = "Product 1", Description = "Description 1", Quantity = 5 });
            Products.Add(new Product { ID = "2", Name = "Product 2", Description = "Description 2", Quantity = 2 });
            Products.Add(new Product { ID = "3", Name = "Product 3", Description = "Description 3", Quantity = 8 });
            Products.Add(new Product { ID = "4", Name = "Product 4", Description = "Description 4", Quantity = 12 });
            Products.Add(new Product { ID = "5", Name = "Product 5", Description = "Description 5", Quantity = 3 });
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
