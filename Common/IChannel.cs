using System;
using System.Threading.Tasks;

namespace Whisper.Common
{
    public interface IChannel<TPackage> : IChannel
    {
        ValueTask SendAsync(TPackage package);
    }

    public interface IChannel
    {
        Task StartAsync();
        Task StopAsync(CloseReason closeReason);
        ValueTask SendAsync(ReadOnlyMemory<byte> data);
    }
}