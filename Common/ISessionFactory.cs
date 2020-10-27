
using System;
using System.Net;

namespace Whisper.Common
{
    public interface ISessionFactory
    {
        ISession Create(IChannel channel);
    }
}