using System.Buffers;

namespace Whisper.Server
{
    public class DefaultHeaderPackage<THeader> : IHeaderPackage<THeader> where THeader : IPackageHeader
    {
        public THeader Header { get; set; }

        public ReadOnlySequence<byte> Body { get; set; }
    }
}