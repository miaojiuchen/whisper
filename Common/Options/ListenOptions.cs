using System.Net;

namespace Whisper.Common
{
    public class ListenOptions
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public static IPEndPoint GetListenEndPoint()
        {

        }
    }
}