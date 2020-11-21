using System;
using System.Buffers;

namespace Whisper.Server
{
    public abstract class HeaderPackageFilter<TPackage, THeader> : PipePackageFilter<TPackage>
        where TPackage : class, IHeaderPackage<THeader>, new()
        where THeader : IPackageHeader
    {
        private THeader _header;

        public HeaderPackageFilter(HeaderPackageDecoder<TPackage, THeader> decoder) : base(decoder)
        {
        }

        public override bool Filter(ref SequenceReader<byte> reader, out TPackage package)
        {
            package = null;

            if (_header == null)
            {
                if (!TryReadHeader(ref reader, out _header))
                {
                    return false;
                }
            }

            var totalSize = _header.ContentLength;

            if (reader.Length < totalSize)
            {
                return false;
            }

            package = DecodePackage(reader.Sequence.Slice(0, totalSize));

            return true;
        }

        protected abstract bool TryReadHeader(ref SequenceReader<byte> reader, out THeader header);
    }
}