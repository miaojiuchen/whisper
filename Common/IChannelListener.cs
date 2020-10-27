
using System.Threading.Tasks;

namespace Whisper.Common
{
    public delegate void NewChannelAcceptedHandler(IChannelListener listener, IChannel channel);

    public interface IChannelListener
    {
        bool IsRunning { get; }

        Task StartAsync();

        Task StopAsync();

        event NewChannelAcceptedHandler OnNewChannelAccepted;
    }
}
