using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.ServiceReferenceEmp;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var emp = new EmpInfoServiceClient();
            Console.WriteLine("Message from servce " + emp.GetEmpSalary("EMP001"));
            Console.WriteLine();
        }
    }
}
