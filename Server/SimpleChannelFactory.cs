using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    /// <summary>
    /// Simple delegate factory
    /// </summary>
    public delegate ValueTask<IChannel<TPackage>> SimpleChannelFactory<TPackage>(Socket socket);
}