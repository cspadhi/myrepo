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
    class Program2
    {
        static void Main2(string[] args)
        {
            //Function1();
            //Function2();

            Thread t1 = new Thread(Function1);
            Thread t2 = new Thread(Function2);

            t1.Start();
            t2.Start();
        }

        private static void Function1()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Function 1 executed " + i.ToString());

                //Wait for 4 sec
                Thread.Sleep(4000);
            }
        }

        private static void Function2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Function 2 executed " + i.ToString());

                //Wait for 4 sec
                Thread.Sleep(4000);
            }
        }
    }
}
