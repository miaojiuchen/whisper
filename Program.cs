using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Whisper.Hosting;

namespace Whisper
{
    class Program
    {
        static void Main(string[] args)
        {
            WhisperHost
                .CreateDefaultBuilder(args)
                .ConfigureLogging((HostBuilderContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConsole();
                })
                .Build()
                .Run();
        }
    }
}