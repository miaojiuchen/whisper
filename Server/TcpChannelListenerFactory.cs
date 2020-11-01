using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpChannelListenerFactory : IChannelListenerFactory
    {
        private ILogger<TcpChannelListenerFactory> _logger;

        public TcpChannelListenerFactory(ILogger<TcpChannelListenerFactory> logger)
        {
            _logger = logger;
        }

        public IChannelListener Create<TPackage>(ListenOptions listenOptions, ServerOptions serverOptions)
        {
            ChannelFactory channelFactory = socket =>
            {
                return new ValueTask<IChannel>(new TcpPipeChannel<TPackage>(null));
            };

            return new TcpChannelListener(listenOptions, channelFactory);
        }
    }
}