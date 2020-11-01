using System;

namespace Whisper.Common
{
    public class ChannelOptions
    {
        public IServiceProvider ApplicationServices { get; set; }

        public int ReceiveTimeout { get; set; }

        public int SendTimeout { get; set; }
    }
}