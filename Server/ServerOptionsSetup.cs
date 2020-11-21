using System;
using Microsoft.Extensions.Options;
using Whisper.Common;

namespace Whisper.Server
{
    public class ServerOptionsSetup<TPackage> : IConfigureOptions<ServerOptions<TPackage>>
    {
        private readonly IServiceProvider _services;

        public ServerOptionsSetup(IServiceProvider services)
        {
            _services = services;
        }

        public void Configure(ServerOptions<TPackage> options)
        {
            options.ApplicationServices = _services;
        }
    }
}