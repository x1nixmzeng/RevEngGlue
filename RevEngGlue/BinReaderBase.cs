using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RevEngGlue
{
    public class BinReaderBase : ISignedReads, IUnsignedReads, IRealReads
    {
        BinaryReader br;

        public BinReaderBase(string filename)
        {
            br = new BinaryReader(File.OpenRead(filename));
        }

        ~BinReaderBase()
        {
            br.BaseStream.Close();
        }

        public bool Opened
        {
            get
            {
                return br.BaseStream.CanRead;
            }
        }

        public int FileSize
        {
            get
            {
                return (int)br.BaseStream.Length;
            }
        }

        public int Position
        {
            get
            {
                return (int)br.BaseStream.Position;
            }
            set
            {
                br.BaseStream.Position = (long)value;
            }
        }

        // Signed integers

        public sbyte i8()
        {
            return br.ReadSByte();
        }

        public sbyte[] i8(int count)
        {
            var tmp = new List<sbyte>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(i8());
            }
            return tmp.ToArray();
        }

        public short i16()
        {
            return br.ReadInt16();
        }

        public short[] i16(int count)
        {
            var tmp = new List<short>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(i16());
            }
            return tmp.ToArray();
        }

        public int i32()
        {
            return br.ReadInt32();
        }

        public int[] i32(int count)
        {
            var tmp = new List<int>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(i32());
            }
            return tmp.ToArray();
        }

        public long i64()
        {
            return br.ReadInt64();
        }

        public long[] i64(int count)
        {
            var tmp = new List<long>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(i64());
            }
            return tmp.ToArray();
        }

        // Unsigned integers

        public byte u8()
        {
            return br.ReadByte();
        }

        public byte[] u8(int count)
        {
            var tmp = new List<byte>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(u8());
            }
            return tmp.ToArray();
        }

        public ushort u16()
        {
            return br.ReadUInt16();
        }

        public ushort[] u16(int count)
        {
            var tmp = new List<ushort>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(u16());
            }
            return tmp.ToArray();
        }

        public uint u32()
        {
            return br.ReadUInt32();
        }

        public uint[] u32(int count)
        {
            var tmp = new List<uint>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(u32());
            }
            return tmp.ToArray();
        }

        public ulong u64()
        {
            return br.ReadUInt64();
        }

        public ulong[] u64(int count)
        {
            var tmp = new List<ulong>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(u64());
            }
            return tmp.ToArray();
        }

        // Floats

        public float f32()
        {
            return br.ReadSingle();
        }

        public float[] f32(int count)
        {
            var tmp = new List<float>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(f32());
            }
            return tmp.ToArray();
        }

        public double f64()
        {
            return br.ReadDouble();
        }

        public double[] f64(int count)
        {
            var tmp = new List<double>(count);
            for (int i = 0; i < count; ++i)
            {
                tmp.Add(f64());
            }
            return tmp.ToArray();
        }
    }
}
