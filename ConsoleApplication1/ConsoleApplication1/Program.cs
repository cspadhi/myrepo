using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Program
    {
       

        static void Main(string[] args)
        {
            var student = new Student()
            {
                MyProperty1 = 10,
                MyProperty2 = true,
                MyProperty3 = "Hello World",
                MyProperty4 = new People()
                {
                    Id = 1,
                    Name = "CSP"
                }
            };

            student.TestMethod(10);
            student.TestMethod(new People()
            {
                Id = 1,
                Name = "CSP"
            });
            student.TestMethod("CSP");

            //Console.WriteLine("hello");

        }
    }

    public class Student
    {
        public int MyProperty1 { get; set; }
        public bool MyProperty2 { get; set; }
        public string MyProperty3 { get; set; }
        public People MyProperty4 { get; set; }

        public T TestMethod<T>(T prop)
        {
            Type type = typeof(string);
            //if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            if (Nullable.GetUnderlyingType(type) != null)
            {
                Console.WriteLine("Nullable");
            }
            else
            {
                Console.WriteLine("Not Nullable, " + Nullable.GetUnderlyingType(type));
            }

            return default(T);
        }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Method1(int x, int y) : this.Always(int x)
        {
            Console.WriteLine("Hello World");
        }

        public void Method2()
        {

        }

        public int Always(int x)
        {
            Console.WriteLine("Hello Keerthy");

            return 1;
        }
    }
}
