using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevEngGlue.PrelenString;

namespace RevEngGlue
{
    public class BinReader : BinReaderBase, IPLStringReader
    {
        PLStringReader StrReader;

        public BinReader(string filename)
            : base(filename)
        {
            StrReader = new PLStringReader(this);
        }

        public BinReader(string filename, Encoding string_enc)
            : base(filename)
        {
            StrReader = new PLStringReader(this);
            Encoding = string_enc;
        }

        public Encoding Encoding
        {
            get
            {
                return StrReader.Encoding;
            }

            set
            {
                StrReader.Encoding = value;
            }
        }

        public string cstr() { return StrReader.cstr(); }
        public string wcstr() { return StrReader.wcstr(); }
        public string str(int length) { return StrReader.str(length); }
        public string wstr(int length) { return StrReader.wstr(length); }
    }
}
