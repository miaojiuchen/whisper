using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Whisper.Common;
using DefaultPackage = Whisper.Server.FixedLengthHeaderPackage<Whisper.Server.DefaultFormatFixedHeader>;
using DefaultPackageFilter = Whisper.Server.FixedLengthHeaderPackageFilter<Whisper.Server.DefaultFormatFixedHeader>;

namespace Whisper.Server
{
    /// <summary>
    /// Host Builder Extensions
    /// </summary>
    public static class WhisperHostBuilderExtensions
    {
        public static IHostBuilder UseWhisper<TPackage, TPackageFilter>(this IHostBuilder hostBuilder)
            where TPackageFilter : PipePackageFilter<TPackage>
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<ServerOptions<TPackage>>(context.Configuration.GetSection("Whisper"));
                services.ConfigureOptions<ServerOptionsSetup<TPackage>>();
                services.AddSingleton<IChannelListenerFactory<TPackage>, TcpChannelListenerFactory<TPackage, TPackageFilter>>();
                services.AddHostedService<WhisperHostedService<TPackage, TPackageFilter>>();
            })
            .ConfigureDefaultFixedHeaderPackage();
        }

        public static IHostBuilder UseWhisper<TPackage, TPackageFilter>(this IHostBuilder hostBuilder, Action<ServerOptions<TPackage>> options)
            where TPackageFilter : PipePackageFilter<TPackage>, new()
        {
            return hostBuilder.UseWhisper<TPackage, TPackageFilter>().ConfigureWhisper(options);
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
            return hostBuilder.UseWhisper<DefaultPackage, DefaultPackageFilter>();
        }

        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder, Action<ServerOptions<DefaultPackage>> options)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(options);
        }

        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder, Action<HostBuilderContext, ServerOptions<DefaultPackage>> configureOptions)
        {
            return hostBuilder.UseWhisper().ConfigureWhisper(configureOptions);
        }

        public static IHostBuilder ConfigureDefaultFixedHeaderPackage(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                var headerSize = context.Configuration.GetValue<int>("whisper:package:headerSize", DefaultFormatFixedHeader.Size);
                services.AddTransient<PipePackageFilter<DefaultPackage>>(serviceProvider => new DefaultPackageFilter(headerSize));
            });
        }
    }
}