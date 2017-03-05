using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program7
    {
        static void Main7(string[] args)
        {
            Task<int> firstTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Executing 1st task.");
                return 42;
            });

            Task secondTask = firstTask.ContinueWith((n) =>
            {
                Console.WriteLine("Executing 2st task.");
                Console.WriteLine("1st task returned {0}.", n.Result);
            });

            secondTask.Wait();
        }

    }
}
