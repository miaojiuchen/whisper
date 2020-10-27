using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Whisper.Hosting
{
    public static class WhisperHostBuilderExtensions
    {
        public static IHostBuilder UseWhisper(this IHostBuilder hostBuilder)
        {
            return hostBuilder;
        }
    }
}