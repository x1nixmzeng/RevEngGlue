using System;
using System.Linq;

namespace RevEngGlue
{
    interface ISignedReads
    {
        sbyte i8();
        short i16();
        int i32();
        long i64();

        sbyte[] i8(int count);
        short[] i16(int count);
        int[] i32(int count);
        long[] i64(int count);
    }

    interface ISignedWrites
    {
        void i8(sbyte val);
        void i16(short val);
        void i32(int val);
        void i64(long val);
    }

    interface IUnsignedReads
    {
        byte u8();
        ushort u16();
        uint u32();
        ulong u64();

        byte[] u8(int count);
        ushort[] u16(int count);
        uint[] u32(int count);
        ulong[] u64(int count);
    }

    interface IRealReads
    {
        float f32();
        double f64();

        float[] f32(int count);
        double[] f64(int count);
    }
}
