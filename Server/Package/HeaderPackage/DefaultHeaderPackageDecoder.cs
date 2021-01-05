using System;
using System.Buffers;

namespace Whisper.Server
{
    public class DefaultHeaderPackageDecoder<THeader> : IPackageDecoder<DefaultHeaderPackage<THeader>>
        where THeader : IPackageHeader, new()
    {
        public DefaultHeaderPackage<THeader> Decode(ReadOnlySequence<byte> bytes)
        {
            return new DefaultHeaderPackage<THeader>
            {
                Body = bytes
            };
        }
    }
}