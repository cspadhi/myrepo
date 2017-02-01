using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class MessageService
    {
        public void OnVideoEncoded(Object Source, VideoEventArgs e)
        {
            Console.WriteLine("MessageService: Sending a text message..." + e.Video.Title);
        }

        public void OnVideoDecoded(Object Source, EventArgs e)
        {
            Console.WriteLine("MessageService: Sending a text message...");
        }
    }
}
