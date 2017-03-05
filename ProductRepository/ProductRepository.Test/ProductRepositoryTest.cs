using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductRepository.Core;
using ProductRepository.Infrastructure;

namespace ProductRepository.Test
{
    [TestClass]
    public class ProductRepositoryTest
    {
        ProductRepository.Infrastructure.ProductRepository Repo;

        [TestInitialize]
        public void TestSetup()
        {
            ProductInitialize db = new ProductInitialize();
            System.Data.Entity.Database.SetInitializer(db);
            Repo = new ProductRepository.Infrastructure.ProductRepository();
        }

        [TestMethod]
        public void IsRepositoryInitializeWithValidNumberOfData()
        {
            var result = Repo.GetProducts();
            Assert.IsNotNull(result);

            var numberOfRecords = result.ToList().Count;

            Assert.AreEqual(2, numberOfRecords);
        }

        [TestMethod]
        public void IsRepositoryAddProduct()
        {
            Product productToInsert = new Product
            {
                Id = 1,
                Name = "Salt",
                InStock = true,
                Price = 22
            };

            Repo.Add(productToInsert);

            var result = Repo.GetProducts();
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(3, numberOfRecords);
        }
    }
}
