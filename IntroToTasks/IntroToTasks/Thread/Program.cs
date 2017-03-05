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
    class Program
    {
        static Maths objMaths = new Maths();
        static void Main(string[] args)
        {
            Thread t1 = new Thread(objMaths.Divide);
            t1.Start(); // Child thread

            objMaths.Divide(); // main thread
        }
    }

    class Maths
    {
        public int num1;
        public int num2;

        Random rand = new Random();
        
        public void Divide()
        {
            lock(this) // Thread safe - Lock, Mutex, Semaphore
            {
                for (long i = 0; i < 100000; i++)
                {
                    num1 = rand.Next(1, 2); // 1 to 2
                    num2 = rand.Next(1, 2);

                    int result = num1 / num2;

                    num1 = 0; // to zero
                    num2 = 0; // child thread
                }
            }
        }
    }
}
