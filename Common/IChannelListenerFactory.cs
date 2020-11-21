namespace Whisper.Common
{
    public interface IChannelListenerFactory
    {
        IChannelListener<TPackage> Create<TPackage>(ListenOptions listenOptions, ServerOptions<TPackage> serverOptions);
    }
}