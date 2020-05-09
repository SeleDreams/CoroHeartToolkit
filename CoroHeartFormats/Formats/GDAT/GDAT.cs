using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoroHeartFormats
{
    public class GDAT : IDisposable
    {
        public static byte[] Magic = new byte[] { 0x47, 0x44, 0x41, 0x54 };
        public GDAT(string path)
        {
            if (File.Exists(path))
            {
                LoadFile(path);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        private void LoadFile(string path)
        {
            _reader = new BinaryReader(File.OpenRead(path));
            byte[] tempMagic = _reader.ReadBytes(4);
            bool isGdat = CheckMagic(tempMagic) ;
            if (isGdat)
            {
                _filesCount = _reader.ReadInt32();
                List<GDATFile> files = new List<GDATFile>();
                for (int i = 0; i < _filesCount; i++)
                {
                    uint offset = (uint)_reader.ReadInt32();
                    if (offset > _reader.BaseStream.Length)
                    {
                        Console.WriteLine("error");
                    }
                    uint length = (uint)_reader.ReadInt32();
                    long pos = _reader.BaseStream.Position;
                   _reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    byte[] magic = _reader.ReadBytes(4);
                    _reader.BaseStream.Seek(pos, SeekOrigin.Begin);
                    GDATFile file = new GDATFile(i,offset ,length ,magic ,this,_reader);
                    files.Add(file);
                }
                _files = files;
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        public bool CheckMagic(byte[] magic)
        {
            return  magic.SequenceEqual(Magic);
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

       /* public ValueTask DisposeAsync()
        {
            return _reader.DisposeAsync();
        }
        */
        public List<GDATFile> Files => _files;
        private List<GDATFile> _files;
        public int FilesCount => _filesCount;
        private int _filesCount;
        private BinaryReader _reader;

    }
}
