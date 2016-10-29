using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevEngGlue
{
    namespace PrelenString
    {
        public struct StringMeta
        {
            public string String;
            public int StringLength;
            public int StringSize;
        }

        interface IPLStringReaderBase
        {
            StringMeta cstr();
            StringMeta wcstr();

            StringMeta str(int length);
            StringMeta wstr(int length);
        }

        interface IPLStringReader
        {
            string cstr();
            string wcstr();

            string str(int length);
            string wstr(int length);
        }
    }
}
