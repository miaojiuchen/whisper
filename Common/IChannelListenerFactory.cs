namespace Whisper.Common
{
    public interface IChannelListenerFactory<TPackage>
    {
        IChannelListener<TPackage> Create(ListenOptions listenOptions, ServerOptions<TPackage> serverOptions);
    }
}