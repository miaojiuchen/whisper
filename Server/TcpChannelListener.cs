using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpChannelListener<TPackage> : IChannelListener<TPackage>
    {
        public bool IsRunning { get; private set; }
        private Socket _listenSocket;
        private readonly ListenOptions _options;
        private readonly SimpleChannelFactory<TPackage> _channelFactory;
        public event NewChannelAcceptedHandler<TPackage> OnNewChannelAccepted;

        public TcpChannelListener(ListenOptions options, SimpleChannelFactory<TPackage> channelFactory)
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