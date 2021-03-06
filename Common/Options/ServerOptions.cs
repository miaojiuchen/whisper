using System;
using System.Collections.Generic;
using System.Text;
using whisper.Common.Options;

namespace Whisper.Common
{
    public partial class ServerOptions<TPackage> : ChannelOptions<TPackage>
    {
        public PackageOptions Package { get; set; }

        public List<ListenOptions> Listeners { get; set; }

        public event Action OnServerReady;

        /// <summary>
        /// Used for trigger event internally
        /// </summary>
        /// <param name="package"></param>
        /// <param name="session"></param>
        internal void TriggerServerReady()
        {
            this.OnServerReady?.Invoke();
        }
    }
}