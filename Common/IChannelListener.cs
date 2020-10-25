
using System.Threading.Tasks;

namespace Whisper.Common
{
    public delegate void NewClientAcceptedHandler(IChannelListener listener);

    public interface IChannelListener
    {
        bool IsRunning { get; }

        Task StartAsync();

        Task StopAsync();

        event NewClientAcceptedHandler OnNewClientAccepted;
    }
}
