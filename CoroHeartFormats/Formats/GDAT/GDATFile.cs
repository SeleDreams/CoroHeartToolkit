using System;
using System.Collections.Generic;
using System.Text;

namespace CoroHeart.Formats.GDAT
{
    public class GDATFile
    {
        public GDATFile(uint offset, uint length)
        {
            this.offset = offset;
            this.length = length;
        }

        public uint Offset { get => this.offset; }
        public uint Length { get => this.length; }


        private uint offset = 0;
        private uint length = 0;
    }
}
