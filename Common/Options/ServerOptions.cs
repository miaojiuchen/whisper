using System;
using System.Collections.Generic;
using System.Text;
using whisper.Common.Options;

namespace Whisper.Common
{
    public delegate void OnPackageReceived<TPackage>(TPackage package, ISession session);

    public class ServerOptions<TPackage> : ChannelOptions
    {
        public PackageOptions Package { get; set; }
        public List<ListenOptions> Listeners { get; set; }
#pragma warning disable CS0067
        public event OnPackageReceived<TPackage> OnPackageReceived;
#pragma warning restore CS0067
    }
}