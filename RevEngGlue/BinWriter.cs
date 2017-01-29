using System.IO;

namespace RevEngGlue
{
    public class BinWriter
    {
        public BinaryWriter bw;

        /// <summary>
        /// Construct a new instance from a filename
        /// </summary>
        public BinWriter(string filename)
        {
            // stub

            bw = new BinaryWriter(File.Create(filename));
        }
    }
}
