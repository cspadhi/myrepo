using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program3
    {
        static void Main3(string[] args)
        {
            Task.Factory.StartNew(() => Speak("Hello World")).Wait();
        }

        private static void Speak(string words)
        {
            Console.WriteLine(words);
        }
    }
}
