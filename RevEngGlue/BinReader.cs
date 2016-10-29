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

        public StringMeta cstr() { return StrReader.cstr(); }
        public StringMeta wcstr() { return StrReader.wcstr(); }
        public StringMeta str(int length) { return StrReader.str(length); }
        public StringMeta wstr(int length) { return StrReader.wstr(length); }
    }
}
