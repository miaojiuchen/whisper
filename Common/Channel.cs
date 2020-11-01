
using System;
using System.Threading.Tasks;

namespace Whisper.Common
{
    public abstract class Channel<TPackage> : IChannel<TPackage>
    {
        protected ChannelOptions Options { get; }

        public abstract ValueTask SendAsync(TPackage package);
        public abstract Task StartAsync();
        public abstract Task StopAsync(CloseReason closeReason);

        protected ValueTask SendAsync(ReadOnlyMemory<byte> data)
        {
            return default;
        }
    }
}