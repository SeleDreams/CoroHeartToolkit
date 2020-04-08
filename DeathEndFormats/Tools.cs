using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace DeathEndFormats
{
    public static class Tools
    {
        public static int BigEndianToLittleEndian(int BigInt)
        {
            byte[] intBytes = BitConverter.GetBytes(BigInt);
            Array.Reverse(intBytes);
            return BitConverter.ToInt32(intBytes, 0);
        }

        public static void Decompress(byte[] data, Stream output)
        {
            using (MemoryStream compressedFileStream = new MemoryStream(data))
            {
                using (DeflateStream decompressionStream = new DeflateStream(compressedFileStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(output);
                }
            }
        }
    }
}