using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program2
    {
        static void Main2(string[] args)
        {
            Task.Factory.StartNew(WhatTypeOfThreadAmI).Wait();
        }

        private static void WhatTypeOfThreadAmI()
        {
            Console.WriteLine("I am a {0} thread", Thread.CurrentThread.IsThreadPoolThread ? "Thread pool" : "Custom");
        }
    }
}
