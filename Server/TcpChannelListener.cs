using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpChannelListener : IChannelListener
    {
        public bool IsRunning { get; private set; }
        public event NewClientAcceptedHandler OnNewClientAccepted;

        private readonly ListenOptions _options;
        private readonly Func<Socket, IChannel> _channelFactory;
        private readonly Func<Socket, ValueTask<IChannel>> _channelFactoryAsync;

        public TcpChannelListener(ListenOptions options, Func<Socket, ValueTask<IChannel>> channelFactory)
        {
            _options = options;
            _channelFactoryAsync = channelFactory;
        }

        public TcpChannelListener(ListenOptions options, Func<Socket, IChannel> channelFactory)
        {
            _options = options;
            _channelFactory = channelFactory;
        }

        public Task StartAsync()
        {
            var endpoint = _options
            var sockert = new Socket();
        }

        public Task StopAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}