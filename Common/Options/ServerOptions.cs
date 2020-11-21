using System;
using System.Collections.Generic;
using System.Text;

namespace Whisper.Common
{
    public delegate void OnPackageReceived<TPackage>(TPackage package, ISession session);

    public class ServerOptions<TPackage> : ChannelOptions
    {
        public List<ListenOptions> Listeners { get; set; }

        public Encoding DefaultEncoding { get; set; }

        public event OnPackageReceived<TPackage> OnPackageReceived;
    }
}