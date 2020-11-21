using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whisper.Server;

namespace whisper
{
    public class MyPackage
    {

    }

    public class MyPackageFilter : PipePackageFilter<MyPackage>
    {
        public override bool Filter(ref SequenceReader<byte> reader, out MyPackage package)
        {
            throw new NotImplementedException();
        }
    }
}
