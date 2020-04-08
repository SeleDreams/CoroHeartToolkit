using System;
using System.Collections.Generic;
using System.Text;

namespace DeathEndFormats
{
    interface ICanExtract
    {
        ulong Extract(byte[] data);
        ulong ExtractAsync(byte[] data);
    }
}
