using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Whisper.Common;

namespace Whisper.Server
{
    /// <summary>
    /// Host Builder Extensions
    /// </summary>
    public static class WhisperHostBuilderExtensions
    {
        public static IHostBuilder UseWhisper<TPackage, TPackageFilter>(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<ServerOptions<TPackage>>(context.Configuration.GetSection("Whisper"));
                services.ConfigureOptions<ServerOptionsSetup>();
                services.AddSingleton<IChannelListenerFactory, TcpChannelListenerFactory>();
            })
            .ConfigurePackageDefaults<FixedLengthHeaderPackage<DefaultFormatHeader>, FixedLengthHeaderPackageFilter<DefaultFormatHeader>>();
        }

        public static IHostBuilder UseWhisper<TPackage, TPackageFilter>(this IHostBuilder hostBuilder, Action<ServerOptions<TPackage>> options)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(options);
        }

        public static IHostBuilder ConfigurePackageDefaults<TPackage, TPackageFilter>(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddHostedService<WhisperHostedService<TPackage, TPackageFilter>>();
            });
        }

        public static IHostBuilder ConfigureWhisper<TPackage>(this IHostBuilder hostBuilder, Action<ServerOptions<TPackage>> options)
        {
            return hostBuilder.ConfigureServices((hostBuilderContext, serivces) =>
            {
                serivces.Configure(options);
            });
        }

        public static IHostBuilder ConfigureWhisper<TPackage>(this IHostBuilder hostBuilder, Action<HostBuilderContext, ServerOptions<TPackage>> configureOptions)
        {
            return hostBuilder.ConfigureServices((hostBuilderContext, services) =>
            {
                services.Configure<ServerOptions<TPackage>>(options =>
                {
                    configureOptions(hostBuilderContext, options);
                });
            });
        }
    }

    /// <summary>
    /// Default Implementation, Use Fixed-Header package format
    /// </summary>
    public static class WhisperHostBuilderExtensionsDefaults
    {
        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseWhisper<FixedLengthHeaderPackage<DefaultFormatHeader>, FixedLengthHeaderPackageFilter<DefaultFormatHeader>>();
        }

        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder, Action<ServerOptions<FixedLengthHeaderPackage<DefaultFormatHeader>>> options)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(options);
        }

        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder, Action<HostBuilderContext, ServerOptions<FixedLengthHeaderPackage<DefaultFormatHeader>>> configureOptions)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(configureOptions);
        }
    }
}