using System.Buffers;

namespace Whisper.Server
{
    public abstract class HeaderPackageDecoder<TPackage, THeader> : IPackageDecoder<TPackage>
        where TPackage : IHeaderPackage<THeader>, new()
        where THeader : IPackageHeader
    {
        public abstract THeader DecodeHeader(ReadOnlySequence<byte> bytes);

        public TPackage Decode(ReadOnlySequence<byte> bytes)
        {
            var header = DecodeHeader(bytes);

            var bodySequence = bytes.Slice(0, header.ContentLength);

            var package = new TPackage();

            package.Header = header;

            package.Body = bodySequence;

            return package;
        }
    }
}