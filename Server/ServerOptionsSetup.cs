using System;
using Microsoft.Extensions.Options;
using Whisper.Common;

namespace Whisper.Server
{
    public class ServerOptionsSetup : IConfigureOptions<ServerOptions>
    {
        private readonly IServiceProvider _services;

        public ServerOptionsSetup(IServiceProvider services)
        {
            _services = services;
        }

        public void Configure(ServerOptions options)
        {
            options.ApplicationServices = _services;
        }
    }
}