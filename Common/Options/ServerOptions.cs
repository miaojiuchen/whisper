using System.Collections.Generic;
using System.Text;

namespace Whisper.Common
{
    public class ServerOptions
    {
        public List<ListenOptions> Listeners { get; set; }

        public ChannelOptions ChannelOptions { get; set; }

        public Encoding DefaultEncoding { get; set; }
    }
}