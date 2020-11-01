using System;
using System.Threading.Tasks;

namespace Whisper.Common
{
    public interface IChannel<TPackage>
    {
        Task StartAsync();
        Task StopAsync(CloseReason closeReason);
        ValueTask SendAsync(TPackage package);
    }
}