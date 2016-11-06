using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RevEngGlue.PrelenString;

namespace RevEngGlue
{
    public class BinReader : BinReaderBase, IPLStringReader
    {
        PLStringReader StrReader;

        public BinReader(string filename)
            : base(File.OpenRead(filename))
        {
            StrReader = new PLStringReader(this);
        }

        public BinReader(string filename, Encoding string_enc)
            : base(File.OpenRead(filename))
        {
            StrReader = new PLStringReader(this, string_enc);
        }

        public BinReader(MemoryStream ms, Encoding string_enc)
            : base(ms)
        {
            StrReader = new PLStringReader(this, string_enc);
        }

        public Encoding StringEncoding
        {
            get { return StrReader.Encoding; }
            set { StrReader.Encoding = value; }
        }

        public StringMeta cstr() { return StrReader.cstr(); }
        public StringMeta wcstr() { return StrReader.wcstr(); }
        public StringMeta str(int length) { return StrReader.str(length); }
        public StringMeta wstr(int length) { return StrReader.wstr(length); }
    }
}
