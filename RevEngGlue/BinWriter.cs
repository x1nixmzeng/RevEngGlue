using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RevEngGlue
{
    public class BinWriter
    {
        public BinaryWriter bw;

        public BinWriter(string filename)
        {
            // stub

            bw = new BinaryWriter(File.Create(filename));
        }
    }
}
