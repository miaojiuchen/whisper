using System;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Whisper.Common;

namespace Whisper.Server
{
    public abstract class PipeChannel<TPackage> : Channel<TPackage>
    {
        public Pipe Outgoing { get; }

        public Pipe Incoming { get; }

        private ILogger _logger;

        private PipePackageFilter<TPackage> _packageFilter;

        protected PipeChannel(ChannelOptions options, PipePackageFilter<TPackage> packageFilter) : base(options)
        {
            Outgoing = new Pipe();
            Incoming = new Pipe();
            _packageFilter = packageFilter;
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

        public override ValueTask SendAsync(TPackage package)
        {
            throw new NotImplementedException();
        }

        private async Task ProcessReads()
        {

        }

        private async Task ProcessWrites()
        {

        }
    }
}