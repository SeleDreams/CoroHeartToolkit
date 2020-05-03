using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace CoroHeart.Formats
{
    public class ZLIB
    {
        const string MAGIC = "BILZ";

        public static void Load(Stream data, Stream output)
        {
            data.Seek(data.Position + 0x80, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(data, Encoding.UTF8, true);
            using (reader)
            {
                long originalPosition = reader.BaseStream.Position;
                string MAGIC = Encoding.UTF8.GetString(reader.ReadBytes(4));
                bool correctFormat = MAGIC == ZLIB.MAGIC;
                if (!correctFormat)
                {
                    throw new InvalidDataException();
                }

                int unknown = reader.ReadInt32();
                int Length = Tools.BigEndianToLittleEndian(reader.ReadInt32());
                int unknown2 = reader.ReadInt32();
                short args = reader.ReadInt16();

                Tools.Decompress(reader.ReadBytes(Length - 2),output);

            }
        }
    }
}