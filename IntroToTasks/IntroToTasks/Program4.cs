using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program4
    {
        static void Main4(string[] args)
        {
            Console.WriteLine("Calling Function");
            Task<int> t = Task.Factory.StartNew(() => Add(500, 300));
            Console.WriteLine("waiting for result");
            Console.WriteLine(t.Result);
        }

        private static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
