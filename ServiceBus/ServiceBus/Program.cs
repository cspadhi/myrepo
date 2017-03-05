using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class Program
    {
        static void Main()
        {
            //MessageQueue messageQueue = new MessageQueue();
            //messageQueue.SendMessage();
            //messageQueue.ReadMessage();

            SessionMessageQueue sessionMessageQueue = new SessionMessageQueue();
            sessionMessageQueue.SendMessage();
            sessionMessageQueue.ReadMessage();
        }
    }
}
