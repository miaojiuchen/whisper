using Whisper.Common;

namespace Whisper.Server
{
    public class DefaultSessionFactory : ISessionFactory
    {
        public ISession Create(IChannel channel)
        {
            throw new System.NotImplementedException();
        }
    }
}