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

        public IChannelListener Create(ListenOptions listenOptions, ServerOptions serverOptions)
        {
            Func<Socket, IChannel> channelFactory = socket =>
            {
                return new TcpChannel(serverOptions.ChannelOptions);
            };

            return new TcpChannelListener(listenOptions, channelFactory);
        }
    }
}