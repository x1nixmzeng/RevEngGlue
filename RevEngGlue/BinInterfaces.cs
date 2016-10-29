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
    }

    interface IUnsignedReads
    {
        byte u8();
        ushort u16();
        uint u32();
        ulong u64();
    }

    interface IRealReads
    {
        float f32();
        double f64();
    }
}
