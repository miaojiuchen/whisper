using System;

namespace Whisper.Common
{
    public delegate void OnPackageReceived<TPackage>(TPackage package, ISession session);

    public partial class ChannelOptions<TPackage>
    {
        public IServiceProvider ApplicationServices { get; set; }

        public int ReceiveTimeout { get; set; }

        public int SendTimeout { get; set; }

        public event OnPackageReceived<TPackage> OnPackageReceived;
    }

    public partial class ChannelOptions<TPackage>
    {
        /// <summary>
        /// Used for trigger event internally
        /// </summary>
        /// <param name="package"></param>
        /// <param name="session"></param>
        internal void TriggerPackageReceive(TPackage package, ISession session)
        {
            this.OnPackageReceived?.Invoke(package, session);
        }
    }
}