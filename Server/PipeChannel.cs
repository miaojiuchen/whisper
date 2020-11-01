using System;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Whisper.Common;

namespace Whisper.Server
{
    public abstract partial class PipeChannel<TPackage> : Channel<TPackage>
    {
        public Pipe Outgoing { get; }

        public Pipe Incoming { get; }

        private ILogger _logger;

        protected PipeChannel(ChannelOptions options)
        {
            Outgoing = new Pipe();
            Incoming = new Pipe();
            _logger = options.ApplicationServices.GetRequiredService<ILogger<PipeChannel<TPackage>>>();
        }

        public override Task StartAsync()
        {
            return Task.WhenAll(ProcessReads(), ProcessWrites());
        }

        public override Task StopAsync(CloseReason closeReason)
        {
            return null;
        }

        public override ValueTask SendAsync(ReadOnlyMemory<byte> data)
        {
            throw new NotImplementedException();
        }

        public override ValueTask SendAsync(TPackage package)
        {
            throw new NotImplementedException();
        }
    }
}