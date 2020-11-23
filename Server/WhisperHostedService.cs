namespace Whisper.Server
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Whisper.Common;
    using System.Linq;
    using System.Collections.Generic;

    internal class WhisperHostedService<TPackage, TPackageFilter> : IHostedService
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ServerOptions<TPackage> _serverOptions;
        private readonly ISessionFactory _sessionFactory;
        private readonly IChannelListenerFactory<TPackage> _channelListenerFactory;
        private readonly List<IChannelListener<TPackage>> _channelListeners = new List<IChannelListener<TPackage>>();

        public WhisperHostedService(IServiceProvider serviceProvider, IOptions<ServerOptions<TPackage>> serverOptions)
        {
            _serverOptions = serverOptions.Value;
            _loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            _logger = _loggerFactory.CreateLogger(nameof(WhisperHostedService<TPackage, TPackageFilter>));
            _sessionFactory = serviceProvider.GetRequiredService<ISessionFactory>();
            _channelListenerFactory = serviceProvider.GetRequiredService<IChannelListenerFactory<TPackage>>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var serverOptions = _serverOptions;

            if (serverOptions.Listeners?.Any() == true)
            {
                foreach (var listenerOption in serverOptions.Listeners)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    var listener = _channelListenerFactory.Create(listenerOption, serverOptions);
                    listener.OnNewChannelAccepted += this.OnNewChannelAccepted;
                    _channelListeners.Add(listener);
                }
            }

            await Task.WhenAll(_channelListeners.Select(x => x.StartAsync()));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_channelListeners.Where(x => x.IsRunning).Select(x => x.StopAsync()));
        }

        private void OnNewChannelAccepted(IChannelListener<TPackage> listener, IChannel<TPackage> channel)
        {
            var session = _sessionFactory.Create(channel);

            Task _ = this.HandleSession(session, channel);
        }

        private async Task HandleSession(ISession session, IChannel<TPackage> channel)
        {
            await foreach (var package in channel.AsAsyncEnumerable())
            {
                _serverOptions.TriggerPackageReceive(package, session);
            }
        }
    }
}