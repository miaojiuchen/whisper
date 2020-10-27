using System;
using System.Net;

namespace Whisper.Common
{
    public class ListenOptions
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public int Backlog { get; set; }
    }

    public static class ListenOptionsExtensions
    {
        public static IPEndPoint GetListenEndPoint(this ListenOptions options)
        {
            var ip = options.IP;
            var port = options.Port;

            IPAddress ipAddress;
            if (ip.Equals("any", StringComparison.OrdinalIgnoreCase))
            {
                ipAddress = IPAddress.Any;
            }
            else if (ip.Equals("ipv6any", StringComparison.OrdinalIgnoreCase))
            {
                ipAddress = IPAddress.IPv6Any;
            }
            else if (ip.Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                ipAddress = IPAddress.Loopback;
            }
            else
            {
                ipAddress = IPAddress.Parse(ip);
            }

            return new IPEndPoint(ipAddress, port);
        }
    }
}