using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebAPI.Models
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products.Add(new Product { Id = "1", Name = "Product 1", Price = 100 });
            products.Add(new Product { Id = "2", Name = "Product 2", Price = 200 });
            products.Add(new Product { Id = "3", Name = "Product 3", Price = 300 });
            products.Add(new Product { Id = "4", Name = "Product 4", Price = 400 });
        }

        public IEnumerable<Product> FindAll()
        {
            return products;
        }

        public Product Find(string id)
        {
            return products.Single(p => p.Id.Equals(id));
        }
    }
}