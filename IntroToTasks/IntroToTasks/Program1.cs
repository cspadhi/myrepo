using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program1
    {
        static void Main1(string[] args)
        {
            Task t = new Task(Speak);
            t.Start();
            Console.WriteLine("Waiting for completion");
            t.Wait();
        }

        private static void Speak()
        {
            Console.WriteLine("Hello World");
        }
    }
}
