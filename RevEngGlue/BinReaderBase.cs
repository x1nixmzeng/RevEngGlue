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

        public short i16()
        {
            return br.ReadInt16();
        }

        public int i32()
        {
            return br.ReadInt32();
        }

        public long i64()
        {
            return br.ReadInt64();
        }

        // Unsigned integers

        public byte u8()
        {
            return br.ReadByte();
        }

        public ushort u16()
        {
            return br.ReadUInt16();
        }

        public uint u32()
        {
            return br.ReadUInt32();
        }

        public ulong u64()
        {
            return br.ReadUInt64();
        }

        // Floats

        public float f32()
        {
            return br.ReadSingle();
        }

        public double f64()
        {
            return br.ReadDouble();
        }

        // Chunks

        public byte[] block(int length)
        {
            return br.ReadBytes(length);
        }

        public byte[] chunk8(int length)
        {
            return br.ReadBytes(length);
        }

        public ushort[] chunk16(int length)
        {
            var tmp = new List<UInt16>();
            
            for(int i =0; i < length; ++i)
            {
                tmp.Add(br.ReadUInt16());
            }

            return tmp.ToArray();
        }
    }
}
