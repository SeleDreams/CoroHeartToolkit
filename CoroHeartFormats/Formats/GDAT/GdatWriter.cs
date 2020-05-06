using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kaitai
{
    public partial class Gdat
    {
        public static event Action<File> onOffsetRecalculation;

        public static FileStream ToFile(Gdat data, string filename)
        {
            data.Write(filename);
            return (FileStream)data.m_io.BaseStream;
        }

        public static Gdat Create(Stream input,Stream output)
        {
            Gdat inputGdat = Gdat.FromStream(input);
            inputGdat.ReadData();
            Gdat outputGdat = Gdat.Create(output);

            foreach (var file in inputGdat.Files)
            {
                Console.WriteLine("Adding file");
                File outputFile = new File(outputGdat.m_io);
                outputFile.FileLength = file.FileLength;
                outputFile.FileOffset = file.FileOffset;
                outputFile.Body = file.Body;
                outputGdat.Files.Add(outputFile);
            }
            return outputGdat;
        }
        public static Gdat Create (Stream output)
        {
            Gdat gdat = new Gdat(new KaitaiStream(output));
            gdat._files = new List<File>();
            return gdat;
        }

        public void RecalculateAllOffsets()
        {
            uint DataBeginning = (uint)(0x08 + (Files.Count * 8));
            uint previousLengthPlusOffset = DataBeginning;
            foreach (File file in Files)
            {
                file.FileOffset = previousLengthPlusOffset;
                file.FileLength = (uint)file.Body.Length;
                previousLengthPlusOffset = file.FileOffset + file.FileLength;
            }
        }

        public static void ToStream(Gdat data, Stream output)
        {
            data.Write(output);
        }

        public void Write(Stream stream)
        {
            m_io = new KaitaiStream(stream);
            _write();
        }
        public void Write(string path)
        {
            m_io = new KaitaiStream(System.IO.File.Create(path));
            _write();
        }
        private void _write()
        {
            
            _magic = new byte[] { 71, 68, 65, 84 };
            Stream baseStream = m_io.BaseStream;
            baseStream.Write(_magic, 0, 4);
            baseStream.Write(BitConverter.GetBytes(FileCount), 0, 4);
            foreach (File file in Files)
            {
                Console.WriteLine("WRITING FILE");
                file.Write();
            }
            for (int id = 0; id < FileCount; id++)
            {
                baseStream.Write(_files[id].Body, 0, (int)_files[id].FileLength);
            }
        }

        public partial class File : KaitaiStruct
        {
            public void SetParent(Gdat gdat)
            {
                m_root = gdat;
                m_parent = gdat;
            }
            private void _recalculateOffsets(uint newLength)
            {
              /*  int currentID = m_root.Files.FindIndex(f => f == this) + 1;
                int change = (int)newLength - (int)_fileLength;
                for (; currentID < m_root.FileCount; currentID++)
                {
                    int offset = (int)m_root._files[currentID]._fileOffset;
                    offset += change;
                    m_root._files[currentID]._fileOffset = (uint)offset;
                }
                if (onOffsetRecalculation != null)
                {
                    onOffsetRecalculation(this);
                }*/
            }

            public void Write()
            {
                m_io.BaseStream.Write(BitConverter.GetBytes(_fileOffset), 0, 4);
                m_io.BaseStream.Write(BitConverter.GetBytes(_fileLength), 0, 4);
            }
        }


    }
}
