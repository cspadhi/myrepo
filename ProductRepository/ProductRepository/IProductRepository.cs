using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Core
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Edit(Product product);
        void Remove(int id);
        IEnumerable<Product> GetProducts();
        Product FindById(int id);
    }
}
