using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Whisper.Common;

namespace Whisper.Server
{
    public static class WhisperHostBuilderExtensions
    {
        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<ServerOptions>(context.Configuration.GetSection("Whisper"));
                services.ConfigureOptions<ServerOptionsSetup>();
                services.AddSingleton<IChannelListenerFactory, TcpChannelListenerFactory>();

                services.AddHostedService<WhisperHostedService>();
            })
            .ConfigurePackageDefaults();
        }

        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder, Action<ServerOptions> options)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(options);
        }

        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder, Action<HostBuilderContext, ServerOptions> configureOptions)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(configureOptions);
        }

        public static IHostBuilder ConfigurePackageDefaults(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton
            });
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