using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public class TcpPipeChannel<TPackage> : PipeChannel<TPackage>
    {
        public Socket _socket;

        public TcpPipeChannel(Socket socket, ChannelOptions<TPackage> options, PipePackageFilter<TPackage> pipePackageFilter) : base(options, pipePackageFilter)
        {
            _socket = socket;
        }

        public override IAsyncEnumerable<TPackage> AsAsyncEnumerable()
        {
            throw new NotImplementedException();
        }

        protected override async ValueTask<int> FillPipeWriterMemory(Memory<byte> memory)
        {
            return await _socket.ReceiveAsync(memory, SocketFlags.None);
        }
    }
}