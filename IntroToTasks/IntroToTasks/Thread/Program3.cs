using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToThreads
{
    class Program3
    {
        static void Main3(string[] args)
        {
            Thread t1 = new Thread(Function1);
            t1.IsBackground = true; // Background Thread. Comment this line to make it foreground thread.
            t1.Start();

            Console.WriteLine("The main application has exited");
        }

        private static void Function1()
        {
            Console.WriteLine("Function 1 is entered");
            Thread.Sleep(5000);
            Console.WriteLine("Function 1 is exited");
        }

        
    }
}
