using System;
using System.IO;
using System.Text;
namespace DeathEndFormats.Formats.GDAT
{
    public class GDAT : IDisposable
    {
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
        public void Export(int id, string path)
        {
            GDATFile data = Files[id];
            Data.Seek(data.Offset, SeekOrigin.Begin);
            using (BinaryReader reader = new BinaryReader(Data, Encoding.UTF8, true))
            {
                byte[] bytes = reader.ReadBytes(4);
                string fileFormat = Encoding.UTF8.GetString(bytes);
                reader.BaseStream.Seek(data.Offset, SeekOrigin.Begin);
                using (FileStream fs = File.Create(path + "." + ReverseString(fileFormat).Replace('\0', '.')))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        reader.BaseStream.Seek(data.Offset, SeekOrigin.Begin);
                       writer.BaseStream.Seek(0, SeekOrigin.Begin);
                       while (reader.BaseStream.Position < data.Offset + 0x80)
                        {
                            // writer.Write(reader.ReadByte());
                            reader.ReadByte();
                        }
                      //  Array.Reverse(bytes);
                       // writer.Write(bytes);
                        ZLIB.Load(Data, writer.BaseStream);
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
