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
        uint available_bits;
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

        /// <summary>
        /// Check that the file has been opened and can be read
        /// </summary>
        public bool Opened
        {
            get
            {
                return br.CanRead;
            }
        }

        /// <summary>
        /// Get the size of the file
        /// </summary>
        public uint FileSize
        {
            get
            {
                return (uint)(br.Length & 0xFFFFFFFF);
            }
        }

        /// <summary>
        /// Get the offset the file is being read from
        /// </summary>
        public uint Position
        {
            get
            {
                return (uint)(br.Position & 0xFFFFFFFF);
            }
            set
            {
                br.Position = value;
            }
        }

        /// <summary>
        /// Get the number of bytes available to read from the current offset
        /// </summary>
        public uint Available
        {
            get
            {
                return FileSize - Position;
            }
        }

        /// <summary>
        /// Check that there is no more data to be read
        /// </summary>
        public bool Eof
        {
            get
            {
                return Position == FileSize;
            }
        }

        /// <summary>
        /// Property which defines the order of bytes for how integers are interpreted
        /// </summary>
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

        /// <summary>
        /// #private Check that a number of bytes can be read from the current position
        /// </summary>
        private void CanReadInternal(uint num)
        {
            if (num > Available)
            {
                throw new Exception(string.Format("Cannot read {0} byte(s) with only {1} remaining", num, Available));
            }
        }

        /// <summary>
        /// #private Read a number of bytes into the internal swap cache, handling endian
        /// </summary>
        private void ReadInternal(uint num)
        {
            CanReadInternal(num);
            br.Read(swap, 0, (int)num);
            available_bits = 0;

            if ((num > 1) && big_endian)
            {
                Array.Reverse(swap, 0, (int)num);
            }
        }

        /// <summary>
        /// #private Read a number of bits, if required
        /// </summary>
        private void ReadInternalBits(uint num)
        {
            if (available_bits == 0)
            {
                ReadInternal(num / 8);
                available_bits = num;
            }
        }

        /// <summary>
        /// Read an signed 8-bit integer from the next byte of data
        /// </summary>
        public sbyte i8()
        {
            ReadInternal(sizeof(sbyte));
            return (sbyte)swap[0];
        }

        /// <summary>
        /// Read an array of signed 8-bit integer values
        /// </summary>
        public sbyte[] i8(uint count)
        {
            CanReadInternal(count * sizeof(sbyte));
            var tmp = new sbyte[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i8();
            }
            return tmp;
        }

        /// <summary>
        /// Read an signed 16-bit integer from the next 2 bytes of data
        /// </summary>
        public short i16()
        {
            ReadInternal(sizeof(short));

            return BitConverter.ToInt16(swap, 0);
        }

        /// <summary>
        /// Read an array of signed 16-bit integer values
        /// </summary>
        public short[] i16(uint count)
        {
            CanReadInternal(count * sizeof(short));
            var tmp = new short[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i16();
            }
            return tmp;
        }

        /// <summary>
        /// Read an signed 32-bit integer from the next 4 bytes of data
        /// </summary>
        public int i32()
        {
            ReadInternal(sizeof(int));

            return BitConverter.ToInt32(swap, 0);
        }

        /// <summary>
        /// Read an array of signed 32-bit integer values
        /// </summary>
        public int[] i32(uint count)
        {
            CanReadInternal(count * sizeof(int));
            var tmp = new int[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i32();
            }
            return tmp;
        }

        /// <summary>
        /// Read an signed 64-bit integer from the next 8 bytes of data
        /// </summary>
        public long i64()
        {
            ReadInternal(sizeof(long));

            return BitConverter.ToInt64(swap, 0);
        }

        /// <summary>
        /// Read an array of signed 64-bit integer values
        /// </summary>
        public long[] i64(uint count)
        {
            CanReadInternal(count * sizeof(long));
            var tmp = new long[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = i64();
            }
            return tmp;
        }

        /// <summary>
        /// Read an unsigned 8-bit integer from the next byte of data
        /// </summary>
        public byte u8()
        {
            ReadInternal(sizeof(byte));
            return swap[0];
        }

        /// <summary>
        /// Read an array of unsigned 8-bit integer values
        /// </summary>
        public byte[] u8(uint count)
        {
            CanReadInternal(count * sizeof(byte));
            var tmp = new byte[count];

            br.Read(tmp, 0, (int)count);

            return tmp;
        }

        /// <summary>
        /// Read an unsigned 16-bit integer from the next 2 bytes of data
        /// </summary>
        public ushort u16()
        {
            ReadInternal(sizeof(ushort));

            return BitConverter.ToUInt16(swap, 0);
        }

        /// <summary>
        /// Read an array of unsigned 16-bit integer values
        /// </summary>
        public ushort[] u16(uint count)
        {
            CanReadInternal(count * sizeof(ushort));
            var tmp = new ushort[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = u16();
            }
            return tmp;
        }

        /// <summary>
        /// Read an unsigned 32-bit integer from the next 4 bytes of data
        /// </summary>
        public uint u32()
        {
            ReadInternal(sizeof(uint));

            return BitConverter.ToUInt32(swap, 0);
        }

        /// <summary>
        /// Read an array of unsigned 32-bit integer values
        /// </summary>
        public uint[] u32(uint count)
        {
            CanReadInternal(count * sizeof(uint));
            var tmp = new uint[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = u32();
            }
            return tmp;
        }

        /// <summary>
        /// Read an unsigned 64-bit integer from the next 8 bytes of data
        /// </summary>
        public ulong u64()
        {
            ReadInternal(sizeof(ulong));

            return BitConverter.ToUInt64(swap, 0);
        }

        /// <summary>
        /// Read an array of unsigned 64-bit integer values
        /// </summary>
        public ulong[] u64(uint count)
        {
            CanReadInternal(count * sizeof(ulong));
            var tmp = new ulong[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = u64();
            }
            return tmp;
        }

        /// <summary>
        /// Read a float from the next 4 bytes of data
        /// </summary>
        public float f32()
        {
            ReadInternal(sizeof(float));

            return BitConverter.ToSingle(swap, 0);
        }

        /// <summary>
        /// Read an array of float values
        /// </summary>
        public float[] f32(uint count)
        {
            CanReadInternal(count * sizeof(float));
            var tmp = new float[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = f32();
            }
            return tmp;
        }

        /// <summary>
        /// Read a double from the next 8 bytes of data
        /// </summary>
        public double f64()
        {
            ReadInternal(sizeof(double));

            return BitConverter.ToDouble(swap, 0);
        }

        /// <summary>
        /// Read an array of double values
        /// </summary>
        public double[] f64(uint count)
        {
            CanReadInternal(count * sizeof(double));
            var tmp = new double[count];
            for (int i = 0; i < count; ++i)
            {
                tmp[i] = f64();
            }
            return tmp;
        }

        /// <summary>
        /// Read a subset of bits from the next byte of data
        /// Subsequent calls will use any remaining bits
        /// </summary>
        public int b8(uint count)
        {
            const int wb = sizeof(byte) * 8;

            count = Math.Min(wb, count);
            ReadInternalBits(wb);

            int val = swap[0];

            int rem = (wb - (int)available_bits);
            val >>= rem;

            available_bits = Math.Max(0, available_bits - count);

            int mask = ~0 << (int)count;
            val &= ~mask;

            return val;
        }

        /// <summary>
        /// Read a subset of bits from the next 2 bytes of data
        /// Subsequent calls will use any remaining bits
        /// </summary>
        public int b16(uint count)
        {
            const int wb = sizeof(short) * 8;

            count = Math.Min(wb, count);
            ReadInternalBits(wb);

            int val = BitConverter.ToInt16(swap, 0);

            int rem = (wb - (int)available_bits);
            val >>= rem;

            available_bits = Math.Max(0, available_bits - count);

            int mask = ~0 << (int)count;
            val &= ~mask;

            return val;
        }

        /// <summary>
        /// Read a subset of bits from the next 4 bytes of data
        /// Subsequent calls will use any remaining bits
        /// </summary>
        public int b32(uint count)
        {
            const int wb = sizeof(int) * 8;

            count = Math.Min(wb, count);
            ReadInternalBits(wb);

            int val = BitConverter.ToInt32(swap, 0);

            int rem = (wb - (int)available_bits);
            val >>= rem;

            available_bits = Math.Max(0, available_bits - count);

            int mask = ~0 << (int)count;
            val &= ~mask;

            return val;
        }

        /// <summary>
        /// Read any remaining bits
        /// </summary>
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
