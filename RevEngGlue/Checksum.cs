namespace RevEngGlue
{
    public enum Checksum
    {
        BYTE,
        SHORT_LE,
        SHORT_BE,
        INT_LE,
        INT_BE,
        INT64_LE,
        INT64_BE,
        SUM8,
        SUM16,
        SUM32,
        SUM64,
        CRC16,
        CRCCCITT,
        CRC32,
        ADLER32,
    }

    public enum ChecksumAlg
    {
        BYTE,
        SHORT_LE,
        SHORT_BE,
        INT_LE,
        INT_BE,
        INT64_LE,
        INT64_BE,
        SUM8,
        SUM16,
        SUM32,
        SUM64,
        CRC16,
        CRCCCITT,
        CRC32,
        ADLER32,
        MD2,
        MD4,
        MD5,
        RIPEMD160,
        SHA1,
        SHA256,
        SHA512,
        TIGER,
    }
}
