
using System;
using System.Net;

namespace Whisper.Common
{
    public interface ISession
    {
        string ID { get; }

        DateTimeOffset ConnectTime { get; }

        DateTimeOffset HeartbeatTime { get; }

        EndPoint RemoteEndPoint { get; }

        EndPoint LocalEndPoint { get; }
    }
}