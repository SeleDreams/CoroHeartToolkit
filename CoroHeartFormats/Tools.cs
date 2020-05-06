using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ionic.Zlib;

namespace CoroHeartFormats
{
    public static class Tools
    {
        public static int BigEndianToLittleEndian(int BigInt)
        {
            byte[] intBytes = BitConverter.GetBytes(BigInt);
            Array.Reverse(intBytes);
            return BitConverter.ToInt32(intBytes, 0);
        }
        public static ushort ReverseShort(ushort test)
        {
            byte[] intBytes = BitConverter.GetBytes(test);
            Array.Reverse(intBytes);
            return BitConverter.ToUInt16(intBytes, 0);
        }

        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream compressedFileStream = new MemoryStream(data))
            {
                using (DeflateStream decompressionStream = new DeflateStream(compressedFileStream, CompressionMode.Decompress))
                {
                    using (MemoryStream memory = new MemoryStream())
                    {

                        decompressionStream.CopyTo(memory);
                        return memory.ToArray();
                    }
                }
            }
        }

        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream decompressedFileStream = new MemoryStream(data))
            {
                using (DeflateStream compressionStream = new DeflateStream(decompressedFileStream, CompressionMode.Compress))
                {
                    using (MemoryStream memory = new MemoryStream())
                    {

                        compressionStream.CopyTo(memory);
                        return memory.ToArray();
                    }
                }
            }
        }
    }
}