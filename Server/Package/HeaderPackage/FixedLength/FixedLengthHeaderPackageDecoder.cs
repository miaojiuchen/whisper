using System;
using System.Buffers;

namespace Whisper.Server
{
    public class FixedLengthHeaderPackageDecoder<THeader> : HeaderPackageDecoder<FixedLengthHeaderPackage<THeader>, THeader>
        where THeader : IPackageHeader, new()
    {
        private readonly int _headerSize;

        public FixedLengthHeaderPackageDecoder(int headerSize)
        {
            _headerSize = headerSize;
        }

        public override THeader DecodeHeader(ReadOnlySequence<byte> bytes)
        {
            if (bytes.Length != _headerSize)
            {
                throw new InvalidOperationException($"Require {_headerSize} bytes fixed header");
            }

            var header = new THeader();

            if (bytes.FirstSpan.Length < _headerSize)
            {
                Span<byte> spanBytes = stackalloc byte[_headerSize];
                bytes.CopyTo(spanBytes);
                header.LoadFromBytes(spanBytes);
            }
            else
            {
                header.LoadFromBytes(bytes.FirstSpan);
            }

            return header;
        }
    }
}