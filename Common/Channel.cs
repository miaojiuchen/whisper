
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Whisper.Common
{
    public abstract class Channel<TPackage> : IChannel<TPackage>
    {
        protected ChannelOptions<TPackage> Options { get; }

        protected Channel(ChannelOptions<TPackage> options)
        {
            this.Options = options;
        }

        public abstract ValueTask SendAsync(TPackage package);
        public abstract Task StartAsync();
        public abstract Task StopAsync(CloseReason closeReason);
        ValueTask IChannel.SendAsync(ReadOnlyMemory<byte> data)
        {
            throw new NotImplementedException();
        }
        
        public abstract IAsyncEnumerable<TPackage> AsAsyncEnumerable();
    }
}