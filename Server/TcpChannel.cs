using Whisper.Common;

namespace Whisper.Server
{
    public class TcpChannel : IChannel
    {
        private readonly ChannelOptions _options;

        public TcpChannel(ChannelOptions options)
        {
            _options = options;
        }

        
    }
}