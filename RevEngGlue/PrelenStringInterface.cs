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

        interface IPLStringReader
        {
            StringMeta cstr();
            StringMeta wcstr();

            StringMeta str(int length);
            StringMeta wstr(int length);
        }
    }
}
