using ProductRepository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        ProductContext context = new ProductContext();
        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Edit(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Product FindById(int id)
        {
            var result = (from product in context.Products where product.Id == id select product).FirstOrDefault();
            return result;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products.ToList();
        }

        public void Remove(int id)
        {
            Product product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
