using System;
using System.IO;
using System;  
using System.Windows.Controls;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Internal;
namespace build
{
    

    public class kFileWrite
    {
        BinaryWriter _writer;
        string _ext;

        public kFileWrite(string name, string ext)
        {
            _ext = ext;
            _writer = new BinaryWriter(new MemoryStream());
        }

        public BinaryWriter io
        {
            get
            {
                return _writer;
            }
        }
    }
    public class kFile
    {
        EndianBinaryReader _reader;
        

        public kFile(byte[] buffer)
        {
            _reader = new EndianBinaryReader(new MemoryStream(buffer));
        }

        public kFile(Stream stream)
        {
            _reader = new EndianBinaryReader(stream);
        }

        public void SetLittleEdian()
        {
            _reader.SetLittleEdian();
        }

        public void SetBigEdian()
        {
            _reader.SetBigEdian();
        }

        public bool ReachedEndOfBuffer
        {
            get
            {
                if (_reader.BaseStream.Position >= _reader.BaseStream.Length)
                    return true;

                return false;
            }
        }

        public int Length
        {
            get
            {
                return (int)_reader.BaseStream.Length;
            }
        }

        public int Position
        {
            get
            {
                return (int)_reader.BaseStream.Position;
            }
        }

        public string ReadFile()
        {
            char[] rawbuffer = _reader.ReadChars((int)_reader.BaseStream.Length);
            return new string(rawbuffer);
        }

        public byte[] kread(int numbytes)
        {
            return _reader.ReadBytes(numbytes);
        }

        public void kread(ref short[] buffer, int numbytes)
        {
            for (int i = 0; i < numbytes; i++)
            {
                buffer[i] = _reader.ReadInt16();
            }
        }

        public void SetPosition(int position)
        {
            _reader.BaseStream.Position = _reader.BaseStream.Position + position;
        }

        public void kread<T, S>(ref T[] ptr, int ptrbase,  int size) where S : IComparable
        {
            if (typeof(S) == typeof(int))
            {
                for (int i = 0; i < size / sizeof(int); i++)
                {
                    if (typeof(T) == typeof(int))
                    {
                        ptr[i + ptrbase] = (T)(object)(int)_reader.ReadInt32();
                    }
                    else
                    {
                        throw new Exception("kread unknown second type");
                    }
                }
            }
            else
            {
                throw new Exception("Kread unknown type");
            }
        }
        /*
        public void kread<T, S>(PointerObject<T> ptr, int size) where S : IComparable
        {
            if (typeof(S) == typeof(int))
            {
                for (int i = 0; i < size / sizeof(int); i++)
                {
                    if (typeof(T) == typeof(int))
                    {
                        ptr[i].memory = (T)(object)(int)_reader.ReadInt32();
                    }
                    else
                    {
                        throw new Exception("kread unknown second type");
                    }
                }
            }
            else
            {
                throw new Exception("Kread unknown type");
            }
        }
        */

        public void kread2<T>(ref T[,] ptr, int basepos, int startpos, int size)
        {
            if (typeof(T) == typeof(short))
            {
                for (int i = 0; i < size / sizeof(short); i++)
                {
                    object val = _reader.ReadInt16();
                    ptr[basepos, startpos + i] = (T)val;
                }
            }
            else if (typeof(T) == typeof(byte))
            {
                for (int i = 0; i < size / sizeof(byte); i++)
                {
                    object val = _reader.ReadByte();
                    ptr[basepos, startpos + i] = (T)val;
                }
            }
            else
            {
                throw new Exception("Kread unknown type");
            }
        }

        public void kread<T>(ref T[] ptr, int startpos, int size)
        {
            if (typeof(T) == typeof(short))
            {
                for (int i = 0; i < size / sizeof(short); i++)
                {
                    object val = _reader.ReadInt16();
                    ptr[startpos + i] = (T)val;
                }
            }
            else if (typeof(T) == typeof(byte))
            {
                for (int i = 0; i < size / sizeof(byte); i++)
                {
                    object val = _reader.ReadByte();
                    ptr[startpos + i] = (T)val;
                }
            }
            else
            {
                throw new Exception("Kread unknown type");
            }
        }
        /*
        public void kread<T>(PointerObject<T> ptr, int size)
        {
            if (typeof(T) == typeof(short))
            {
                for (int i = 0; i < size / sizeof(short); i++)
                {
                    object val = _reader.ReadInt16();
                    ptr[i].memory = (T)val;
                }
            }
            else if (typeof(T) == typeof(byte))
            {
                for (int i = 0; i < size / sizeof(byte); i++)
                {
                    object val = _reader.ReadByte();
                    ptr[i].memory = (T)val;
                }
            }
            else
            {
                throw new Exception("Kread unknown type");
            }
        }
        */
        public sbyte kreadsbyte()
        {
            return _reader.ReadSByte();
        }

        public byte kreadbyte()
        {
            return _reader.ReadByte();
        }

        public short kreadshort()
        {
            return _reader.ReadInt16();
        }

        public int kreadint()
        {
            return _reader.ReadInt32();
        }

        public void kreadshort(out short val)
        {
            val = _reader.ReadInt16();
        }
        public void kreadint(out int val)
        {
            val = _reader.ReadInt32();
        }

        public void Close()
        {
            _reader.Dispose();
        }
    }

    public class kFileSystem
    {
        List<bGrpArchive> grpfiles = new List<bGrpArchive>();

        //
        // ReadContentFile
        //

        /*
         * The filelist file contains a list of files that are outside of the grp. This is to allow us to search for files
         * in the content directory as well as check to see if they exist or not because try catch on a StreamOpen takes
         * too int when it throws a filenotfoundexception.
         * 
        */
        public byte[] ReadContentFile(string filepath)
        {
            BinaryReader reader = new BinaryReader(Application.GetResourceStream(new Uri("base/" + filepath, UriKind.RelativeOrAbsolute)).Stream);

            byte[] buffer = reader.ReadBytes((int)reader.BaseStream.Length);

            reader.Dispose();
            return buffer;
        }

        public Stream ReadContentFileStream(string filepath)
        {
            return Application.GetResourceStream(new Uri("base/" + filepath, UriKind.RelativeOrAbsolute)).Stream;
        }

        //
        // ContentFileExists
        //
        private bool ContentFileExists(string filepath)
        {
            return File.Exists("base/" + filepath);
        }
        public byte[] kreadfile(string name)
        {
       //     if (ContentFileExists(name))
        //    {
            try
            {
                return ReadContentFile(name);
            }
            catch (System.IO.FileNotFoundException)
            {

            }
      //      }

            name = name.ToUpper();
            for (int i = 0; i < grpfiles.Count; i++)
            {
                byte[] buffer = grpfiles[i].ReadFile(name);
                if (buffer != null)
                {
                    return buffer;
                }
            }

            return null;
        }

        public kFile kopen4load(string name)
        {
          //  if (ContentFileExists(name))
            try
            {
                return new kFile(ReadContentFile(name));
            }
            catch (System.NullReferenceException) { }
            name = name.ToUpper();
            for (int i = 0; i < grpfiles.Count; i++)
            {
                byte[] buffer = grpfiles[i].ReadFile(name);
                if (buffer != null)
                {
                    return new kFile(buffer);
                }
            }

            return null;
        }

        //
        // InitGrpFile
        //
        public void InitGrpFile(string name)
        {
            bGrpArchive grpfile = new bGrpArchive(name);
            grpfiles.Add(grpfile);
        }
    }
}
