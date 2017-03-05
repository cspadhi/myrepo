using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program5
    {
        static void Main5(string[] args)
        {
            TimeIt(SequentialChancesToWin);
            TimeIt(TaskBasedChancesToWin);
        }

        private static void TimeIt(Action action)
        {
            Stopwatch timer = Stopwatch.StartNew();
            action();
            Console.WriteLine(timer.Elapsed);
        }

        private static void TaskBasedChancesToWin()
        {
            int n = 35;
            int r = 5;

            Task<int> part1 = Task<int>.Factory.StartNew(() => Factorial(n));
            Task<int> part2 = Task<int>.Factory.StartNew(() => Factorial(n-r));
            Task<int> part3 = Task<int>.Factory.StartNew(() => Factorial(r));

            int chances = part1.Result / (part2.Result * part3.Result);

            Console.WriteLine(chances);
        }

        private static void SequentialChancesToWin()
        {
            int n = 35;
            int r = 5;

            int part1 = Factorial(n);
            int part2 = Factorial(n - r);
            int part3 = Factorial(r);

            //Console.WriteLine(part1 + ", " + part2 + ", " + part3);

            int chances = part1 / (part2 * part3);

            Console.WriteLine(chances);
        }

        private static int Factorial(int x)
        {
            int factorial = 1;

            for (int i = 1; i <= x; i++)
            {
                factorial = factorial * i;
            }

            return factorial;
        }
    }
}
