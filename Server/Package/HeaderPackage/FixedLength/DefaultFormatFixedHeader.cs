using System;
using System.Buffers.Binary;

namespace Whisper.Server
{
    public class DefaultFormatFixedHeader : IPackageHeader
    {
        public const int Size = 8;
        public int PackageType { get; set; }
        public int ContentLength { get; set; }

        public void LoadFromBytes(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length != Size)
            {
                throw new InvalidOperationException("Wrong header bytes size");
            }

            PackageType = BinaryPrimitives.ReadInt32BigEndian(bytes.Slice(0, 4));
            ContentLength = BinaryPrimitives.ReadInt32BigEndian(bytes.Slice(4, 4));
        }
    }
}