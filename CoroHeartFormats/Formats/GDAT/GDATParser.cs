using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kaitai
{
    public partial class Gdat : KaitaiStruct, IDisposable
    {
        public void Dispose()
        {
            foreach(var file in Files)
            {
                if (file.Body != null)
                {
                    file.Body = null;
                }
            }
        }
        public static Gdat FromFile(string fileName)
        {
            return new Gdat(new KaitaiStream(fileName));
        }
        public static Gdat FromMemory(byte[] data)
        {
            return new Gdat(new KaitaiStream(data));
        }

        public static Gdat FromStream(Stream stream)
        {
            return new Gdat(new KaitaiStream(stream));
        }
        public Gdat(KaitaiStream p__io, KaitaiStruct p__parent = null, Gdat p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
        }

        public void ReadData()
        {
            _read();
        }

        private void _read()
        {
            _magic = m_io.EnsureFixedContents(new byte[] { 71, 68, 65, 84 });
            _fileCount = m_io.ReadU4le();
            _files = new List<File>((int)(_fileCount));
            for (var i = 0; i < _fileCount; i++)
            {
                _files.Add(new File(m_io, this, m_root));
                _files[i].Read();
            }
        }

        public partial class File : KaitaiStruct
        {
            public static File FromFile(string fileName)
            {
                return new File(new KaitaiStream(fileName));
            }

            public File(KaitaiStream p__io, Gdat p__parent = null, Gdat p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_body= false;
            }

            public void Read()
            {
                _read();
            }

            private void _read()
            {
                _fileOffset = m_io.ReadU4le();
                _fileLength = m_io.ReadU4le();
            }

            private bool f_body;
            private byte[] _body;

            byte[] InitBody()
            {
                if (f_body)
                    return _body;
                long _pos = m_io.Pos;
                m_io.Seek(FileOffset);
                magic = new byte[4];
                m_io.Read(magic, 0, 4);
                m_io.Seek(m_io.Pos - 4);
                _body = m_io.ReadBytes(FileLength);
                m_io.Seek(_pos);
                f_body = true;
                return _body;
            }
            public byte[] Body
            {
                get
                {
                    return InitBody();
                }
                set
                {
                    if (value != null)
                    {
                        f_body = true;
                        _body = value;
                        _recalculateOffsets((uint)_body.Length);
                        _fileLength = (uint)_body.Length;
                    }
                    else
                    {
                        f_body = false;
                        _body = null;
                    }
                }
            }
            private byte[] magic;
            private uint _fileOffset;
            private uint _fileLength;
            private Gdat m_root;
            private Gdat m_parent;

            public string Magic
            {
                get { if (magic == null) { InitBody(); } return Encoding.UTF8.GetString(magic); }
            }
            public uint FileOffset
            {
                get { return _fileOffset; }
                set { _fileOffset = value; }
            }
            public uint FileLength
            {
                get { return _fileLength; }
                set { _fileLength = value; }
            }
            public Gdat M_Root { get { return m_root; } }
            public Gdat M_Parent { get { return m_parent; } }
        }
        private byte[] _magic;
        private uint _fileCount;
        private List<File> _files;
        private Gdat m_root;
        private KaitaiStruct m_parent;
        public byte[] Magic { get { return _magic; } }
        public uint FileCount { get { return (uint)Files.Count; } }
        public List<File> Files { get { return _files; } }
        public Gdat M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
