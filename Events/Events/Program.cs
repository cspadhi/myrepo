using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class Program
    {
        static void Main(string[] args)
        {
            var video = new Video { Id = 1001, Title = "Video 1" };

            var videoEncoder = new VideoEncoder(); // Publisher of Event

            var mailService = new MailService(); // Subscriber of Event
            var messageService = new MessageService(); // Subscriber of Event

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

            videoEncoder.Encode(video);

            Console.WriteLine();


            videoEncoder.VideoDecoded += mailService.OnVideoDecoded;
            videoEncoder.VideoDecoded += messageService.OnVideoDecoded;

            videoEncoder.Decode(video);
        }
    }
}
