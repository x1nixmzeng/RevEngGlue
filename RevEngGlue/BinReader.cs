using System.Text;
using System.IO;
using RevEngGlue.PrelenString;

namespace RevEngGlue
{
    public class BinReader : BinReaderBase, IPLStringReader
    {
        PLStringReader StrReader;

        /// <summary>
        /// Construct a new instance from a filename
        /// </summary>
        public BinReader(string filename)
            : base(File.OpenRead(filename))
        {
            StrReader = new PLStringReader(this);
        }

        /// <summary>
        /// Construct a new instance from a filename, specifying the default encoding
        /// </summary>
        public BinReader(string filename, Encoding string_enc)
            : base(File.OpenRead(filename))
        {
            StrReader = new PLStringReader(this, string_enc);
        }

        /// <summary>
        /// Construct a new instance from a memory stream
        /// </summary>
        public BinReader(MemoryStream ms)
            : base(ms)
        {
            StrReader = new PLStringReader(this);
        }

        /// <summary>
        /// Construct a new instance from a memory stream, specifying the deafult encoding
        /// </summary>
        public BinReader(MemoryStream ms, Encoding string_enc)
            : base(ms)
        {
            StrReader = new PLStringReader(this, string_enc);
        }

        /// <summary>
        /// Property which defines how strings should be represented
        /// </summary>
        public Encoding StringEncoding
        {
            get { return StrReader.Encoding; }
            set { StrReader.Encoding = value; }
        }

        /// <summary>
        /// Read a null-terminated string of 8-bits
        /// </summary>
        public StringMeta cstr() { return StrReader.cstr(); }

        /// <summary>
        /// Read a null-terminated string of 16-bits
        /// </summary>
        public StringMeta wcstr() { return StrReader.wcstr(); }

        /// <summary>
        /// Read a string of 8-bits with a known length
        /// </summary>
        public StringMeta str(int length) { return StrReader.str(length); }

        /// <summary>
        /// Read a string of 16-bits with a known length
        /// </summary>
        public StringMeta wstr(int length) { return StrReader.wstr(length); }

        /// <summary>
        /// Read a string using a specific set of parameters
        /// </summary>
        public StringMeta pl(PrelenStringParams param) { return StrReader.pl(param); }
    }
}
