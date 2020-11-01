using System;
using System.Collections.Generic;
using System.Text;

namespace Whisper.Common
{
    public class ServerOptions : ChannelOptions
    {
        public List<ListenOptions> Listeners { get; set; }
        
        public Encoding DefaultEncoding { get; set; }
    }
}