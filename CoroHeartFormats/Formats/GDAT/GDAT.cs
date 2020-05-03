using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace CoroHeart.Formats.GDAT
{
    public class GDAT : IDisposable
    {
        public static bool isGDAT(Stream data)
        {
            byte[] buffer = new byte[4];
            data.Read(buffer, 0, 4);
            data.Seek(0, SeekOrigin.Begin);
            int value = BitConverter.ToInt32(buffer, 0);
            byte[] magicBytes = Encoding.UTF8.GetBytes(MAGIC);
            return value == BitConverter.ToInt32(magicBytes, 0);
        }
        public static GDAT Load(Stream data)
        {
            GDAT GDatData = new GDAT();
            GDatData.Data = data;
            BinaryReader reader = new BinaryReader(data, Encoding.UTF8, true);

            using (reader)
            {
                string MAGIC = Encoding.UTF8.GetString(reader.ReadBytes(GDAT.MAGIC.Length));
                bool correctFormat = GDAT.MAGIC == MAGIC;
                if (!correctFormat)
                {
                    throw new InvalidDataException();
                }

                GDatData.FileCount = reader.ReadUInt32();
                Console.WriteLine("There are " + GDatData.FileCount + " files in the GDAT !");
                GDatData.Files = new GDATFile[GDatData.FileCount];
                for (int i = 0; i < GDatData.FileCount; i++)
                {
                    GDatData.Files[i] = new GDATFile(reader.ReadUInt32(), reader.ReadUInt32());
                    var file = GDatData.Files[i];
                    Console.WriteLine("File found at the address : 0x" + file.Offset.ToString("X") + " =>  Length : " + ((float)file.Length / 1024 / 1024).ToString("n4") + " Mb");
                }
            }
            return GDatData;
        }
        public static string ReverseString(string myStr)
        {
            char[] myArr = myStr.ToCharArray();
            Array.Reverse(myArr);
            return new string(myArr);
        }
        public static string CleanString(string dirtyString)
        {
            HashSet<char> removeChars = new HashSet<char>(" ?&^$#@!()+-,:;<>’\'-_*");
            StringBuilder result = new StringBuilder(dirtyString.Length);
            foreach (char c in dirtyString)
                if (!removeChars.Contains(c)) // prevent dirty chars
                    result.Append(c);
            return result.ToString();
        }

        public static bool isZLIB(byte[] data)
        {
            byte[] magic = new byte[4];
            Array.Copy(data, 0x80, magic, 0, 4);
            return ReverseString(Encoding.UTF8.GetString(magic)) == "ZLIB";
        }

        public void Export(int id, string path)
        {
            GDATFile data = Files[id];
            Console.WriteLine("start export");
            Data.Seek(data.Offset, SeekOrigin.Begin);
            using (BinaryReader reader = new BinaryReader(Data, Encoding.UTF8, true))
            {
                int MAGIC = reader.ReadInt32();
                string fileFormat = ReverseString(Encoding.UTF8.GetString(BitConverter.GetBytes(MAGIC)));
                string fileName = data.Offset + "." + string.Concat(fileFormat.Split(Path.GetInvalidFileNameChars()));
                Console.WriteLine(fileName);
                reader.BaseStream.Seek(data.Offset, SeekOrigin.Begin);
                byte[] buffer = reader.ReadBytes((int)data.Length);
                reader.BaseStream.Seek(data.Offset, SeekOrigin.Begin);
                string finalPath = path + fileName;
                using (FileStream fs = File.Create(finalPath))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        if (isZLIB(buffer))
                        {
                            Console.WriteLine(fileName + " is a zlib archive, extracting...");
                            reader.BaseStream.Seek(data.Offset, SeekOrigin.Begin);
                            ZLIB.Load(Data, writer.BaseStream);
                        }
                        else
                        {
                            Console.WriteLine(fileName + " is an unknown file type, it will be exported as is");
                            reader.BaseStream.Seek(data.Offset, SeekOrigin.Begin);
                            writer.Write(reader.ReadBytes((int)data.Length));
                        }
                    }
                }
            }
        }



        public void Dispose()
        {
            Data.Dispose();
        }

        public const string MAGIC = "GDAT";
        public uint FileCount;
        public GDATFile[] Files;
        public Stream Data;
    }
}
