using System;
using System.Buffers;

namespace Whisper.Server
{
    public abstract class PipePackageFilter<TPackage>
    {
        protected IPackageDecoder<TPackage> Decoder { get; set; }

        public PipePackageFilter()
        {
        }

        public PipePackageFilter(IPackageDecoder<TPackage> decoder)
        {
            Decoder = decoder;
        }

        public abstract bool Filter(ref SequenceReader<byte> reader, out TPackage package);

        protected virtual TPackage DecodePackage(ReadOnlySequence<byte> bytes)
        {
            if (Decoder == null)
            {
                throw new InvalidOperationException("You must set Decoder of PipePackageFilter first");
            }

            return Decoder.Decode(bytes);
        }
    }
}