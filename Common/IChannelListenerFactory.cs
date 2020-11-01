namespace Whisper.Common
{
    public interface IChannelListenerFactory
    {
        IChannelListener Create<TPackage>(ListenOptions listenOptions, ServerOptions serverOptions);
    }
}