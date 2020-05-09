using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
namespace CoroHeartFormats
{
    public class GDATFile
    {
        public GDATFile(int id, ulong offset, ulong size, byte[] magic, GDAT Parent, BinaryReader reader)
        {
            ID = id;
            Offset = offset;
            Size = size;
            Magic = magic;
            GDAT = Parent;
            _reader = reader;
        }

        public void Export(Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                _reader.BaseStream.Seek((long)Offset,SeekOrigin.Begin);
                byte[] file = _reader.ReadBytes((int)Size);
                writer.Write(file);
            }
        }

        public readonly int ID;
        public readonly ulong Offset;
        public readonly ulong Size;
        public readonly byte[] Magic;
        public readonly GDAT GDAT;
        private BinaryReader _reader;
    }
}
