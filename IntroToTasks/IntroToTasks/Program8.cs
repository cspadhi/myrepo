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
    class Program8
    {
        static void Main8(string[] args)
        {
            Task[] tasks = new Task[4];

            for (int i = 0; i < 4; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => System.Threading.Thread.Sleep(5000));
            }

            Task.Factory.ContinueWhenAll(tasks, (t) => Console.WriteLine("Tasks have finished"));
            Console.ReadLine();
        }

    }
}
