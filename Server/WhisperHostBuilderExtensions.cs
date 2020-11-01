using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Whisper.Common;

namespace Whisper.Server
{
    public static class WhisperHostBuilderExtensions
    {
        public static IHostBuilder UseWhisper<TPackage>(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<ServerOptions>(context.Configuration.GetSection("Whisper"));
                services.ConfigureOptions<ServerOptionsSetup>();
                services.TryAddSingleton<IChannelListenerFactory, TcpChannelListenerFactory>();
                // services.TryAddSingleton<ISessionFactory, DefaultSessionFactory>();
                services.AddHostedService<WhisperHostedService>();
            });
        }

        public static IHostBuilder UseWhisper<TPackage>(this IHostBuilder hostBuilder, Action<ServerOptions> options)
        {
            return hostBuilder.UseWhisper<TPackage>().ConfigureWhisper(options);
        }

        public static IHostBuilder UseWhisper<TPackage>(this IHostBuilder hostBuilder, Action<HostBuilderContext, ServerOptions> configureOptions)
        {
            return hostBuilder.UseWhisper<TPackage>().ConfigureWhisper(configureOptions);
        }

        public static IHostBuilder ConfigureWhisper(this IHostBuilder hostBuilder, Action<ServerOptions> options)
        {
            return hostBuilder.ConfigureServices((hostBuilderContext, serivces) =>
            {
                serivces.Configure(options);
            });
        }

        public static IHostBuilder ConfigureWhisper(this IHostBuilder hostBuilder, Action<HostBuilderContext, ServerOptions> configureOptions)
        {
            return hostBuilder.ConfigureServices((hostBuilderContext, services) =>
            {
                services.Configure<ServerOptions>(options =>
                {
                    configureOptions(hostBuilderContext, options);
                });
            });
        }
    }
}