using System;
using System.Buffers;

namespace Whisper.Server
{
    public abstract class HeaderPackageFilter<TPackage, THeader> : PipePackageFilter<TPackage>
        where TPackage : class, IHeaderPackage<THeader>, new()
        where THeader : class, IPackageHeader
    {
        private THeader _header;

        public HeaderPackageFilter(IPackageDecoder<TPackage> decoder) : base(decoder)
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

            var contentLength = _header.ContentLength;

            if (reader.Remaining < contentLength)
            {
                return false;
            }

            package = base.DecodePackage(reader.Sequence.Slice(reader.Position, contentLength));

            package.Header = _header;

            reader.Advance(contentLength);

            _header = null; // reset

            return true;
        }

        protected abstract bool TryReadHeader(ref SequenceReader<byte> reader, out THeader header);
    }
}