using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            RepositoryProduct rp = new RepositoryProduct();
            var products = rp.GetAll();

            RepositoryEmployee re = new RepositoryEmployee();
            var employees = re.GetAll();

            if(Debugger.IsAttached)
            {
                Console.ReadLine();
            }
            //=====================================

            GRepository<Employee> rep = new GRepository<Employee>();
            var result = rep.GetAll();

            GRepository<Product> rep1 = new GRepository<Product>();
            var result1 = rep1.GetAll();

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }
}
