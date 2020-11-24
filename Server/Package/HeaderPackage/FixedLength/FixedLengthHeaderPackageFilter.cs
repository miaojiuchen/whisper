using System.Buffers;

namespace Whisper.Server
{
    public class FixedLengthHeaderPackageFilter<THeader> : HeaderPackageFilter<FixedLengthHeaderPackage<THeader>, THeader>
        where THeader : class, IPackageHeader, new()
    {
        private readonly int _headerSize;

        public FixedLengthHeaderPackageFilter(int headerSize) : base(new FixedLengthHeaderPackageDecoder<THeader>(headerSize))
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

            var bytes = reader.Sequence.Slice(0, _headerSize);
            header = (Decoder as FixedLengthHeaderPackageDecoder<THeader>).DecodeHeader(bytes);

            reader.Advance(_headerSize);

            return true;
        }
    }
}