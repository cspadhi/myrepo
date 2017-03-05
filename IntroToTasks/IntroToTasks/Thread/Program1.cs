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
    class Program1
    {
        static void Main1(string[] args)
        {
            //Thread t1 = new Thread(RunMillionIterations);
            //t1.Start();

            Parallel.For(0, 1000000, x => RunMillionIterations());
        }

        private static void RunMillionIterations()
        {
            string x = string.Empty;
            for(int i = 0; i < 1000000; i++)
            {
                x = x + "s";
            }
        }
    }
}
