namespace RevEngGlue
{
    /// <summary>
    /// Abstract interface for how signed values should be read
    /// </summary>
    interface ISignedReads
    {
        sbyte i8();
        short i16();
        int i32();
        long i64();

        sbyte[] i8(uint count);
        short[] i16(uint count);
        int[] i32(uint count);
        long[] i64(uint count);
    }

    /// <summary>
    /// Abstract interface for how signed values should be written
    /// </summary>
    interface ISignedWrites
    {
        void i8(sbyte val);
        void i16(short val);
        void i32(int val);
        void i64(long val);
    }

    /// <summary>
    /// Abstract interface for how unsigned values should be read
    /// </summary>
    interface IUnsignedReads
    {
        byte u8();
        ushort u16();
        uint u32();
        ulong u64();

        byte[] u8(uint count);
        ushort[] u16(uint count);
        uint[] u32(uint count);
        ulong[] u64(uint count);
    }

    /// <summary>
    /// Abstract interface for how float-point values should be read
    /// </summary>
    interface IRealReads
    {
        float f32();
        double f64();

        float[] f32(uint count);
        double[] f64(uint count);
    }

    /// <summary>
    /// Abstract interface for how bits should be read
    /// </summary>
    interface IBitReads
    {
        int b8(uint count);
        int b16(uint count);
        int b32(uint count);

        int bdiscard();
    }
}
