using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CoroHeart.Formats.CSH
{
    public class CSH
    {
        const string MAGIC = "\0hsc";

        static public bool isCSH(Stream data)
        {
            byte[] buffer = new byte[4];
            data.Read(buffer, 0, 4);
            data.Seek(0, SeekOrigin.Begin);
            int value =BitConverter.ToInt32(buffer,0);
            byte[] magicBytes = Encoding.UTF8.GetBytes(MAGIC);
            return value == BitConverter.ToInt32(magicBytes,0);
        }

        static public void Load(Stream data, Stream output)
        {
            BinaryReader reader = new BinaryReader(data, Encoding.UTF8, true);
            BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, true);
            using (reader)
            {
                using (writer)
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    writer.BaseStream.Seek(0, SeekOrigin.Begin);
                    ZLIB.Load(reader.BaseStream, writer.BaseStream);
                }
            }
        }
    }
}
