using System;
using whisper;
using Whisper.Server;
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using System.Buffers.Binary;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Whisper
{
    class Program
    {
        static void Main(string[] args)
        {
            var _ = Task.Run(() =>
            {
                Host
                .CreateDefaultBuilder(args)
                .UseWhisper(options =>
                {
                    options.OnPackageReceived += (package, session) =>
                    {
                        Console.WriteLine(package.Header.ContentLength);
                        Console.WriteLine(package.Header.PackageType);
                        Console.WriteLine(package.Body.Length);
                    };
                })
                .Build()
                .Run();
            });

            Task.Delay(3000).GetAwaiter().GetResult();

            using TcpClient client = new TcpClient("localhost", 5000);

            var message = Encoding.UTF8.GetBytes("Hello World");
            var contentLength = message.Length;

            byte[] data = new byte[DefaultFormatFixedHeader.Size + contentLength];
            var span = new Span<byte>(data);
            BinaryPrimitives.WriteInt32BigEndian(span.Slice(4), contentLength);
            message.CopyTo(span.Slice(8));

            NetworkStream stream = client.GetStream();
            using StreamWriter sw = new StreamWriter(stream);
            sw.Write(data);
        }
    }
}