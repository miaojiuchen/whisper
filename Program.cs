using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Whisper.Server;

namespace Whisper
{
    class Package
    {
        string Name;

        // string
    }

    class Program
    {
        static void Main(string[] args)
        {
            Host
                .CreateDefaultBuilder(args)
                .UseWhisper<Package>(options =>
                {
                    Console.WriteLine(options);
                    // options.OnPackageReceived += (package, session) =>
                    // {
                    //     session.SendAsync($"{package.Name} Hello world");
                    // };

                    // options.OnPackageReceived += (package, session) =>
                    // {

                    // };
                })
                .Build()
                .Run();
        }
    }
}