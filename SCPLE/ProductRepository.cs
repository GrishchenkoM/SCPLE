using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE
{
    // можно реализовать Singleton
    public class ProductRepository
    {
        public ProductRepository()
        {
            Products = new List<Product>();
        }
        public List<Product> Products;
    }
}
