using System;

namespace Whisper.Server
{
    public interface IPackageHeader
    {
        int ContentLength { get; set; }

        void LoadFromBytes(ReadOnlySpan<byte> bytes);
    }
}