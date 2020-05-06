using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace CoroHeartFormats
{
    public class ZLIB
    {
        const string MAGIC = "BILZ";
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public static string GetFileType(Stream data)
        {
            long pos = data.Position;
            byte[] buffer = new byte[4];
            data.Seek(pos + 0x08, SeekOrigin.Begin);
            data.Read(buffer, 0, 4);
            data.Seek(pos, SeekOrigin.Begin);

            return ByteArrayToString(buffer);
        }
        public static bool isZLIB(Stream data)
        {
            long pos = data.Position;
            byte[] buffer = new byte[4];
            data.Seek(0x80, SeekOrigin.Begin);
            data.Read(buffer, 0, 4);
            data.Seek(0x80, SeekOrigin.Begin);
            int value = BitConverter.ToInt32(buffer, 0);
            byte[] magicBytes = Encoding.UTF8.GetBytes(MAGIC);
            data.Seek(pos, SeekOrigin.Begin);
            return value == BitConverter.ToInt32(magicBytes, 0);
        }

        public static void Create(string file, string output)
        {
            string filename = Path.GetFileName(file);
            string[] infos = filename.Split('.');
            char[] sArray = infos[01].ToCharArray();
            Array.Reverse(sArray);
            string magic = sArray.ToString();
            if (magic.Length != 4)
            {
                magic.Insert(0, "\0");
            }
            byte[] objectType = BitConverter.GetBytes((int)long.Parse(infos[2], System.Globalization.NumberStyles.HexNumber));
            using (BinaryWriter writer = new BinaryWriter(File.Create(output)))
            {
                byte[] magicBytes = Encoding.UTF8.GetBytes(magic);
                int nullBytesToAdd = 4 - magicBytes.Length;
                if (nullBytesToAdd > 0)
                {
                    byte[] outputBytes = new byte[4];
                    for (int i = 0; i < nullBytesToAdd; i++)
                    {
                        outputBytes[i] = 0;
                    }
                    Array.Copy(magicBytes, 0, outputBytes, nullBytesToAdd, 4 - nullBytesToAdd);
                    magicBytes = outputBytes;
                }
                writer.Write(magicBytes);
                writer.Write(Tools.BigEndianToLittleEndian(0x00000002));
                Array.Reverse(objectType);
                writer.Write(objectType);
                writer.Write(Tools.BigEndianToLittleEndian(0x00000002));
                while (writer.BaseStream.Position < 0x28)
                {
                    writer.Write(0);
                }
                writer.Write(Tools.BigEndianToLittleEndian(0x00000001));
                while (writer.BaseStream.Position < 0x80)
                {
                    writer.Write(0);

                }
                writer.Write(Tools.BigEndianToLittleEndian(0x42494C5A));
                byte[] fileData = File.ReadAllBytes(file);
                byte[] compressedData = Tools.Compress(fileData);
                int uncompressedLength = Tools.BigEndianToLittleEndian(fileData.Length);
                int Length = Tools.BigEndianToLittleEndian(compressedData.Length);
                int unknown2 = Tools.BigEndianToLittleEndian(7);
                writer.Write(uncompressedLength);
                writer.Write(Length);
                writer.Write(unknown2);
                writer.Write((byte)0x78);
                writer.Write((byte)0xDA);
                writer.Write(compressedData);
            }
        }
        public static void Load(Stream data, Stream output)
        {
            data.Seek(data.Position + 0x84, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(data, Encoding.UTF8, true);
            using (reader)
            {
                int unknown = reader.ReadInt32();
                int Length = Tools.BigEndianToLittleEndian(reader.ReadInt32());
                int unknown2 = reader.ReadInt32();
                int unknown3 = reader.ReadInt16();
                byte[] decompressedData = Tools.Decompress(reader.ReadBytes(Length - 2));
                output.Write(decompressedData, 0, decompressedData.Length);
            }
        }
    }
}