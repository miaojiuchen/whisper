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

        private PipePackageFilterFactory _pipePackageFilterFactory;

        public TcpChannelListenerFactory(ILogger<TcpChannelListenerFactory> logger, PipePackageFilterFactory pipePackageFilterFactory)
        {
            _logger = logger;
            _pipePackageFilterFactory = pipePackageFilterFactory;
        }

        public IChannelListener Create<TPackage>(ListenOptions listenOptions, ServerOptions serverOptions)
        {
            ChannelFactory channelFactory = socket =>
            {
                return new ValueTask<IChannel>(new TcpPipeChannel<TPackage>(serverOptions, _pipePackageFilterFactory.Create<TPackage>()));
            };

            return new TcpChannelListener(listenOptions, channelFactory);
        }
    }
}