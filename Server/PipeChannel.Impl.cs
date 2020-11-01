using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Server
{
    public abstract partial class PipeChannel<TPackage> : Channel<TPackage>
    {
        private async Task ProcessReads()
        {

        }

        private async Task ProcessWrites()
        {

        }
    }
}