using System;
using Whisper.Common;

namespace Whisper.Server
{
    public class SessionFactory : ISessionFactory
    {
        public ISession Create(IChannel channel)
        {
            var session = new Session(channel)
            {
                StartTime = DateTimeOffset.Now,
                // LocalEndPoint = channel.
            };

            return session;
        }
    }
}