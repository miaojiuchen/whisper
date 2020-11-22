using Microsoft.Extensions.Options;
using whisper.Common.Options;
using Whisper.Common;

namespace Whisper.Server
{
    public class PipePackageFilterFactory<TPackage, TPackageFilter>
        where TPackageFilter : PipePackageFilter<TPackage>, new()
    {
        public PackageOptions _packageOptions;

        public PipePackageFilterFactory(IOptions<ServerOptions<TPackage>> options)
        {

        }

        public PipePackageFilter<TPackage> Create()
        {
            return new TPackageFilter();
        }
    }
}