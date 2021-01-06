using System;
using whisper;
using Whisper.Server;
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using System.Buffers.Binary;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Whisper
{
    class Program
    {
        static void Main(string[] args)
        {
            Host
            .CreateDefaultBuilder(args)
            .UseWhisper(options =>
            {
                options.OnPackageReceived += (package, session) =>
                {
                    Console.WriteLine(Encoding.UTF8.GetString(package.Body.FirstSpan));
                };
                options.OnServerReady += () =>
                {
                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Interval = 1000;

                    var message = Encoding.UTF8.GetBytes("Hello World");
                    var contentLength = message.Length;

                    byte[] data = new byte[DefaultFormatFixedHeader.Size + contentLength];
                    var span = new Span<byte>(data);
                    BinaryPrimitives.WriteInt32BigEndian(span.Slice(4), contentLength);
                    message.CopyTo(span.Slice(8));

                    timer.Elapsed += (sender, e) =>
                    {
                        using TcpClient client = new TcpClient("localhost", 5000);
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);
                        stream.Flush();
                        Task.Delay(1000).Wait();
                    };

                    timer.Start();
                };
            })
            .Build()
            .Run();
        }
    }
}