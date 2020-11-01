using System;
using System.Net;
using Whisper.Common;

namespace Whisper.Server
{
    public class Session : ISession
    {
        private IChannel _channel;

        public Session(IChannel channel)
        {
            _channel = channel;
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; }

        public DateTimeOffset StartTime { get; internal set; }

        public DateTimeOffset UpdateTime { get; internal set; }

        public EndPoint RemoteEndPoint { get; internal set; }

        public EndPoint LocalEndPoint { get; internal set; }
    }
}