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
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IChannelListenerFactory<TPackage> _channelListenerFactory;
        private readonly List<IChannelListener<TPackage>> _channelListeners = new List<IChannelListener<TPackage>>();

        public WhisperHostedService(IServiceProvider serviceProvider, IOptions<ServerOptions<TPackage>> serverOptions, IHostApplicationLifetime hostApplicationLifeTime)
        {
            _serverOptions = serverOptions.Value;
            _loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            _logger = _loggerFactory.CreateLogger(nameof(WhisperHostedService<TPackage, TPackageFilter>));
            _sessionFactory = serviceProvider.GetRequiredService<ISessionFactory>();
            _hostApplicationLifetime = hostApplicationLifeTime;
            _channelListenerFactory = serviceProvider.GetRequiredService<IChannelListenerFactory<TPackage>>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"WhisperHostedService<{nameof(TPackage)}, {nameof(TPackageFilter)}> started");
            
            var serverOptions = _serverOptions;

            if (serverOptions.Listeners?.Any() == true)
            {
                foreach (var listenerOption in serverOptions.Listeners)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return Task.CompletedTask;
                    }

                    var listener = _channelListenerFactory.Create(listenerOption, serverOptions);
                    listener.OnNewChannelAccepted += this.OnNewChannelAccepted;
                    _channelListeners.Add(listener);
                    _logger.LogInformation($"Listener {listenerOption.IP}:{listenerOption.Port} Added");
                }
            }

            // Use ToList to ensure all listener is ready on ports
            var tasks = _channelListeners.Select(x => x.StartAsync()).ToList();
            
            _serverOptions.TriggerServerReady();

            return Task.WhenAll(tasks);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(_channelListeners.Where(x => x.IsRunning).Select(x => x.StopAsync()));
        }

        private void OnNewChannelAccepted(IChannelListener<TPackage> listener, Channel<TPackage> channel)
        {
            _logger.LogInformation($"Channel Accepted");
            var session = _sessionFactory.Create(channel);
            this.HandleSession(session, channel);
            channel.StartAsync();
        }

        private void HandleSession(ISession session, Channel<TPackage> channel)
        {
            channel.OnPackageFiltered += package =>
            {
                _serverOptions.TriggerPackageReceive(package, session);
            };
        }
    }
}