using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CoroHeart.Formats.CSH
{
    public class CSH
    {
        const int MAGIC = 0x00687363;

        static public void Load(Stream data, Stream output)
        {
            BinaryReader reader = new BinaryReader(data, Encoding.UTF8, true);
            BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, true);
            using (reader)
            {
                using (writer)
                {
                    int value = Tools.BigEndianToLittleEndian(reader.ReadInt32());
                    bool correctFormat = value == CSH.MAGIC;
                    if (!correctFormat)
                    {
                        throw new InvalidDataException();
                    }
                    writer.Write(Tools.BigEndianToLittleEndian(value));
                    while (reader.BaseStream.Position < 0x80)
                    {
                        reader.ReadInt32();
                    }
                    ZLIB.Load(reader.BaseStream, writer.BaseStream);
                }
            }
        }
    }
}
