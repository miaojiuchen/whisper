using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpChannelListener : IChannelListener
    {
        public bool IsRunning { get; private set; }
        public event NewChannelAcceptedHandler OnNewChannelAccepted;
        private Socket _listenSocket;
        private readonly ListenOptions _options;
        private readonly ChannelFactory _channelFactory;

        public TcpChannelListener(ListenOptions options, ChannelFactory channelFactory)
        {
            _options = options;
            _channelFactory = channelFactory ?? throw new ArgumentNullException(nameof(channelFactory));
        }

        public async Task StartAsync()
        {
            var endpoint = _options.GetListenEndPoint();
            var listenSocket = _listenSocket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listenSocket.Bind(endpoint);
            listenSocket.Listen(_options.Backlog);

            IsRunning = true;

            while (IsRunning)
            {
                var clientSocket = await listenSocket.AcceptAsync();
                var channel = await _channelFactory(clientSocket);
                OnNewChannelAccepted?.Invoke(this, channel);
            }
        }

        public Task StopAsync()
        {
            IsRunning = false;
            _listenSocket.Close();
            return Task.CompletedTask;
        }
    }
}