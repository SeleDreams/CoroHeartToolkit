using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoroHeartFormats
{
    public class TEX
    {

        public static void ReadData(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                Console.WriteLine("Opening file.");
                Console.WriteLine($"Image identification field : {reader.ReadByte()}\n");
                Console.WriteLine($"Color map type : {reader.ReadByte()}\n");
                Console.WriteLine($"Image type code : {reader.ReadByte()}\n");
                Console.WriteLine("----Color map specification----\n");
                Console.WriteLine($"Color map origin : {Tools.ReverseShort(reader.ReadUInt16())}");
                Console.WriteLine($"Color map length : {Tools.ReverseShort(reader.ReadUInt16())}");
                Console.WriteLine($"Color map entry size : {reader.ReadByte()}");
                Console.WriteLine($"Image infos\n");
                Console.WriteLine($"X Origin : {Tools.ReverseShort(reader.ReadUInt16())}");
                Console.WriteLine($"Y Origin : {Tools.ReverseShort(reader.ReadUInt16())}");
                Console.WriteLine($"Width : {Tools.ReverseShort(reader.ReadUInt16())}");
                Console.WriteLine($"Height : {Tools.ReverseShort(reader.ReadUInt16())}");
                Console.WriteLine($"Pixel size : {reader.ReadByte()}");
                Console.WriteLine($"image descriptor : {reader.ReadByte()}");
            }
        }
    }
}
