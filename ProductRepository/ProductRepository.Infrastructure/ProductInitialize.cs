using ProductRepository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Infrastructure
{
    public class ProductInitialize : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            context.Products.Add(new Product
            {
                Id = 1,
                Name = "Rice",
                InStock = true,
                Price = 55
            });

            context.Products.Add(new Product
            {
                Id = 2,
                Name = "Sugar",
                InStock = false,
                Price = 40
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
