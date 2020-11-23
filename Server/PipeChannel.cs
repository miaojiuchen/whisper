using System;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Whisper.Common;
using System.IO;
using System.Buffers;
using System.Collections.Generic;

namespace Whisper.Server
{
    public abstract class PipeChannel<TPackage> : Channel<TPackage>
    {
        public Pipe Outgoing { get; }

        public Pipe Incoming { get; }

        private ILogger<PipeChannel<TPackage>> _logger;

        private PipePackageFilter<TPackage> _packageFilter;

        protected PipeChannel(ChannelOptions<TPackage> options, PipePackageFilter<TPackage> packageFilter) : base(options)
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

        public override event OnPackageFiltered<TPackage> OnPackageFiltered;

        #region Read

        private async Task ProcessReads()
        {
            var pipe = Incoming;

            Task writePipe = WritePipe(pipe.Writer);
            Task consumePipe = ConsumePipe(pipe.Reader);

            await Task.WhenAll(writePipe, consumePipe);
        }

        private async Task WritePipe(PipeWriter writer)
        {
            while (true)
            {
                try
                {
                    var memory = writer.GetMemory();

                    var bytesRead = await FillPipeWriterMemory(memory);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    writer.Advance(bytesRead);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error occurred when writing pipe");
                    break;
                }

                var result = await writer.FlushAsync();
                if (result.IsCompleted)
                {
                    break;
                }
            }
        }

        private async Task ConsumePipe(PipeReader reader)
        {
            while (true)
            {
                ReadResult result;
                try
                {
                    result = await reader.ReadAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error occurred when reading pipe");
                    break;
                }

                var buffer = result.Buffer;

                ParseBuffer(buffer);
            }
        }

        private void ParseBuffer(ReadOnlySequence<byte> buffer)
        {
            var sequenceReader = new SequenceReader<byte>(buffer);

            while (_packageFilter.Filter(ref sequenceReader, out TPackage package))
            {
                OnPackageFiltered?.Invoke(package);
            }
        }

        protected abstract ValueTask<int> FillPipeWriterMemory(Memory<byte> memory);

        #endregion

        #region Write

        private async Task ProcessWrites()
        {

        }

        #endregion
    }
}