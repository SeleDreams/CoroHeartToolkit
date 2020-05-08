using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CoroHeartFormats
{
    public class GDATFile
    {
        public GDATFile(int id, ulong offset, ulong size,byte[] magic, GDAT Parent)
        {
            ID = id;
            Offset = offset;
            Size = size;
            Magic = magic;
            GDAT = Parent;
        }

        public readonly int ID;
        public readonly ulong Offset;
        public readonly ulong Size;
        public readonly byte[] Magic;
        public readonly GDAT GDAT;
    }
}
