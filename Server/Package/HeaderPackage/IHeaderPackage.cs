using System.Buffers;

namespace Whisper.Server
{
    public interface IHeaderPackage<THeader>
        where THeader : IPackageHeader
    {
        THeader Header { get; set; }
        ReadOnlySequence<byte> Body { get; set; }
    }
}