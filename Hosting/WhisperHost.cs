using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Whisper.Hosting
{
    public static class WhisperHost
    {
        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return new WhisperHostBuilder();
        }
    }
}