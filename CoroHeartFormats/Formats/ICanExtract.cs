using System;
using System.Collections.Generic;
using System.Text;

namespace CoroHeart
{
    interface ICanExtract
    {
        ulong Extract(byte[] data);
        ulong ExtractAsync(byte[] data);
    }
}
