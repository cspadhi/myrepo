using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    public class VideoEncoder
    {
        // =============================================
        // Event with parameter
        // =============================================

        public event EventHandler<VideoEventArgs> VideoEncoded;
        public void Encode(Video video)
        {
            Console.WriteLine("Encoding video....");
            Thread.Sleep(3000);

            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if(VideoEncoded != null)
            {
                VideoEncoded(this, new VideoEventArgs() { Video = video });
            }
        }

        // =============================================
        // Event without parameter
        // =============================================

        public event EventHandler VideoDecoded;
        public void Decode(Video video)
        {
            Console.WriteLine("Decoding video....");
            Thread.Sleep(3000);

            OnVideoDecoded();
        }

        protected virtual void OnVideoDecoded()
        {
            if (VideoEncoded != null)
            {
                VideoDecoded(this, EventArgs.Empty);
            }
        }
    }
}
