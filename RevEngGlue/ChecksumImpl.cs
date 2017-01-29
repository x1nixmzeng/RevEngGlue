using System.Text;
using DamienG.Security.Cryptography;

namespace RevEngGlue
{
    public class ChecksumImpl
    {
        /// <summary>
        /// Calculate the CRC32 checksum from an array of bytes
        /// </summary>
        public static uint crc32(byte[] data)
        {
            return Crc32.Compute(data);
        }

        /// <summary>
        /// Calculate the CRC32 checksum from a string
        /// </summary>
        public static uint crc32(string data)
        {
            return crc32(data, CharSet.ANSI);
        }

        /// <summary>
        /// Calculate the CRC32 checksum from a string with a given encoding
        /// </summary>
        public static uint crc32(string data, Encoding enc)
        {
            return Crc32.Compute(enc.GetBytes(data));
        }
    }
}
