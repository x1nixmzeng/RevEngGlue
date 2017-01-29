using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RevEngGlue
{
    public enum BinBaseEndian
    {
        EndianLittle,
        EndianBig,
    }

    public class BinReaderBase : ISignedReads, IUnsignedReads, IRealReads, IBitReads
    {
        Stream br;
        byte[] swap;
        int available_bits;
        bool big_endian;

        public BinReaderBase(Stream source)
        {
            br = source;
            swap = new byte[10];
            available_bits = 0;
            big_endian = false;
        }

        ~BinReaderBase()
        {
            br.Close();
        }

        public bool Opened
        {
            get
            {
                return br.CanRead;
            }
        }

        public int FileSize
        {
            get
            {
                return (int)br.Length;
            }
        }

        public int Position
        {
            get
            {
                return (int)br.Position;
            }
            set
            {
                br.Position = (long)value;
            }
        }

        public bool Eof
        {
            get
            {
                return Position == FileSize;
            }
        }

        public BinBaseEndian Endian
        {
            get
            {
                return big_endian ? BinBaseEndian.EndianBig : BinBaseEndian.EndianLittle;
            }

            set
            {
                big_endian = (value == BinBaseEndian.EndianBig);
            }
        }

        // Private helpers

        private void ReadInternal(int num)
        {
            br.Read(swap, 0, num);
            available_bits = 0;

            if ((num > 1) && big_endian)
            {
                Array.Reverse(swap, 0, num);
            }
        }

        private void ReadInternalBits(int num)
        {
            if (available_bits == 0)
            {
                ReadInternal(num / 8);
                available_bits = num;
            }
        }

        // Signed integers

        public sbyte i8()
        {
            ReadInternal(sizeof(sbyte));
            return (sbyte)swap[0];
        }

        public sbyte[] i8(int count)
        {
            var tmp = new sbyte[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i8();
            }
            return tmp;
        }

        public short i16()
        {
            ReadInternal(sizeof(short));

            return BitConverter.ToInt16(swap, 0);
        }

        public short[] i16(int count)
        {
            var tmp = new short[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i16();
            }
            return tmp;
        }

        public int i32()
        {
            ReadInternal(sizeof(int));

            return BitConverter.ToInt32(swap, 0);
        }

        public int[] i32(int count)
        {
            var tmp = new int[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i32();
            }
            return tmp;
        }

        public long i64()
        {
            ReadInternal(sizeof(long));

            return BitConverter.ToInt64(swap, 0);
        }

        public long[] i64(int count)
        {
            var tmp = new long[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i64();
            }
            return tmp;
        }

        // Unsigned integers

        public byte u8()
        {
            ReadInternal(sizeof(byte));
            return swap[0];
        }

        public byte[] u8(int count)
        {
            var tmp = new byte[count];

            br.Read(tmp, 0, count);

            return tmp;
        }

        public ushort u16()
        {
            ReadInternal(sizeof(ushort));

            return BitConverter.ToUInt16(swap, 0);
        }

        public ushort[] u16(int count)
        {
            var tmp = new ushort[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = u16();
            }
            return tmp;
        }

        public uint u32()
        {
            ReadInternal(sizeof(uint));

            return BitConverter.ToUInt32(swap, 0);
        }

        public uint[] u32(int count)
        {
            var tmp = new uint[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = u32();
            }
            return tmp;
        }

        public ulong u64()
        {
            ReadInternal(sizeof(ulong));

            return BitConverter.ToUInt64(swap, 0);
        }

        public ulong[] u64(int count)
        {
            var tmp = new ulong[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = u64();
            }
            return tmp;
        }

        // Floats

        public float f32()
        {
            ReadInternal(sizeof(float));

            return BitConverter.ToSingle(swap, 0);
        }

        public float[] f32(int count)
        {
            var tmp = new float[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = f32();
            }
            return tmp;
        }

        public double f64()
        {
            ReadInternal(sizeof(double));

            return BitConverter.ToDouble(swap, 0);
        }

        public double[] f64(int count)
        {
            var tmp = new double[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = f64();
            }
            return tmp;
        }

        // Bits

        public int b8(int count)
        {
            const int wb = sizeof(byte) * 8;

            count = Math.Min(wb, count);
            ReadInternalBits(wb);

            int val = swap[0];

            int rem = (wb - available_bits);
            val >>= rem;

            available_bits = Math.Max(0, available_bits - count);

            int mask = ~0 << count;
            val &= ~mask;

            return val;
        }

        public int b16(int count)
        {
            const int wb = sizeof(short) * 8;

            count = Math.Min(wb, count);
            ReadInternalBits(wb);

            int val = BitConverter.ToInt16(swap, 0);

            int rem = (wb - available_bits);
            val >>= rem;

            available_bits = Math.Max(0, available_bits - count);

            int mask = ~0 << count;
            val &= ~mask;

            return val;
        }

        public int b32(int count)
        {
            const int wb = sizeof(int) * 8;

            count = Math.Min(wb, count);
            ReadInternalBits(wb);

            int val = BitConverter.ToInt32(swap, 0);

            int rem = (wb - available_bits);
            val >>= rem;

            available_bits = Math.Max(0, available_bits - count);

            int mask = ~0 << count;
            val &= ~mask;

            return val;
        }

        public int bdiscard()
        {
            int final = 0;

            if (available_bits > 0)
            {
                final = b32(available_bits);
            }

            available_bits = 0;
            return final;
        }
    }
}
