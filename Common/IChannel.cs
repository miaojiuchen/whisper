using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Whisper.Common
{
    public interface IChannel<TPackage> : IChannel
    {
        ValueTask SendAsync(TPackage package);

        /// <summary>
        /// Get Async Enumerator for keep receiving package loop
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<TPackage> AsAsyncEnumerable();
    }

    public interface IChannel
    {
        Task StartAsync();
        Task StopAsync(CloseReason closeReason);
        ValueTask SendAsync(ReadOnlyMemory<byte> data);
    }
}