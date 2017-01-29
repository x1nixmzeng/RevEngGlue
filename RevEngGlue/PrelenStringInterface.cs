using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Abstract interface for how strings should be read
        /// </summary>
        interface IPLStringReader
        {
            StringMeta cstr();
            StringMeta wcstr();

            StringMeta str(int length);
            StringMeta wstr(int length);

            StringMeta pl(PrelenStringParams param);
        }
    }
}
