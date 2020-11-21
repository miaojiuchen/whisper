using System;
using whisper;
using Whisper.Server;
using Microsoft.Extensions.Hosting;

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
                        Console.WriteLine(package.Header.ContentLength);
                        Console.WriteLine(package.Header.PackageType);
                        Console.WriteLine(package.Body.Length);
                    };
                })
                .Build()
                .Run();

            Host
                .CreateDefaultBuilder(args)
                .UseWhisper<MyPackage, MyPackageFilter>(options =>
                {
                    options.OnPackageReceived += (package, session) =>
                    {
                        Console.WriteLine(package.GetType() == typeof(MyPackage));
                    };
                })
                .Build()
                .Run();
        }
    }
}