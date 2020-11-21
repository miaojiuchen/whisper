
using System.Threading.Tasks;

namespace Whisper.Common
{
    public delegate void NewChannelAcceptedHandler<TPackage>(IChannelListener<TPackage> listener, IChannel channel);

    public interface IChannelListener<TPackage>
    {
        bool IsRunning { get; }

        Task StartAsync();

        Task StopAsync();

        event NewChannelAcceptedHandler<TPackage> OnNewChannelAccepted;
    }
}
