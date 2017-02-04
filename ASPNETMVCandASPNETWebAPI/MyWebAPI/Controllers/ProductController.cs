using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private ProductRepository repo = new ProductRepository();

        public IEnumerable<Product> Get()
        {
            return repo.FindAll();
        }

        public Product Get(string id)
        {
            return repo.Find(id);
        }
    }
}
