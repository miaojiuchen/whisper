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

        protected override bool TryReadHeader(ref SequenceReader<byte> reader, out THeader header)
        {
            header = null;

            if (reader.Length < _headerSize)
            {
                return false;
            }

            header = new THeader();


            if (reader.UnreadSequence.FirstSpan.Length < _headerSize)
            {
                Span<byte> temp = stackalloc byte[_headerSize];
                reader.Sequence.CopyTo(temp);
                header.LoadFromBytes(temp);
            }
            else
            {
                header.LoadFromBytes(reader.UnreadSequence.FirstSpan.Slice(0, _headerSize));
            }

            reader.Advance(_headerSize);

            return true;
        }
    }
}