using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public delegate ValueTask<IChannel> ChannelFactory(Socket socket);
}