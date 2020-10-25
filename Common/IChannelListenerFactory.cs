namespace Whisper.Common
{
    public interface IChannelListenerFactory
    {
        IChannelListener Create(ListenOptions listenOptions, ServerOptions serverOptions);
    }
}