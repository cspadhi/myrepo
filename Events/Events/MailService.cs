using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class MailService
    {
        public void OnVideoEncoded(Object source, VideoEventArgs e)
        {
            Console.WriteLine("MailService: Sending an email..." + e.Video.Title);
        }

        public void OnVideoDecoded(Object source, EventArgs e)
        {
            Console.WriteLine("MailService: Sending an email...");
        }
    }
}
