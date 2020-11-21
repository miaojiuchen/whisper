using System.Buffers;

namespace Whisper.Server
{
    public interface IPackageDecoder<TPackage>
    {
        TPackage Decode(ReadOnlySequence<byte> bytes);
    }
}