using System;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpPipeChannel<TPackage> : PipeChannel<TPackage>
    {
        public TcpPipeChannel(ChannelOptions options, PipePackageFilter<TPackage> pipePackageFilter) : base(options, pipePackageFilter)
        {

        }
    }
}