using System;
using System.Buffers;

namespace Whisper.Server
{
    public class FixedLengthHeaderPackageFilter<THeader> : HeaderPackageFilter<DefaultHeaderPackage<THeader>, THeader>
        where THeader : class, IPackageHeader, new()
    {
        private readonly int _headerSize;

        public FixedLengthHeaderPackageFilter(int headerSize) : base(new DefaultHeaderPackageDecoder<THeader>())
        {
            _headerSize = headerSize;
        }

        protected override bool TryReadHeader(in SequenceReader<byte> reader, out THeader header)
        {
            header = null;

            if (reader.Length < _headerSize)
            {
                return false;
            }

            var seq = reader.Sequence.Slice(0, _headerSize);

            header = new THeader();

            if (seq.FirstSpan.Length < _headerSize)
            {
                Span<byte> temp = stackalloc byte[_headerSize];
                seq.CopyTo(temp);
                header.LoadFromBytes(temp);
            }
            else
            {
                header.LoadFromBytes(seq.FirstSpan);
            }

            reader.Advance(_headerSize);

            return true;
        }
    }
}