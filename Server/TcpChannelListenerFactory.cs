using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpChannelListenerFactory<TPackage, TPackageFilter> : IChannelListenerFactory<TPackage>
        where TPackageFilter : PipePackageFilter<TPackage>
    {
        private ILogger<TcpChannelListenerFactory<TPackage, TPackageFilter>> _logger;

        private PipePackageFilter<TPackage> _pipePackageFilter;

        public TcpChannelListenerFactory(ILogger<TcpChannelListenerFactory<TPackage, TPackageFilter>> logger, PipePackageFilter<TPackage> pipePackageFilter)
        {
            _logger = logger;
            _pipePackageFilter = pipePackageFilter;
        }

        public IChannelListener<TPackage> Create(ListenOptions listenOptions, ServerOptions<TPackage> serverOptions)
        {
            SimpleChannelFactory<TPackage> channelFactory = socket =>
            {
                return new ValueTask<Channel<TPackage>>(new TcpPipeChannel<TPackage>(socket, serverOptions, _pipePackageFilter));
            };

            return new TcpChannelListener<TPackage>(listenOptions, channelFactory);
        }
    }
}