using ProductRepository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Infrastructure
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=ProductConnectionString")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
