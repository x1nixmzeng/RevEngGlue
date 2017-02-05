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
            public uint StringLength;
            public uint StringSize;
        }

        /// <summary>
        /// Abstract interface for how strings should be read
        /// </summary>
        interface IPLStringReader
        {
            StringMeta cstr();
            StringMeta wcstr();

            StringMeta str(uint length);
            StringMeta wstr(uint length);

            StringMeta pl(PrelenStringParams param);
        }
    }
}
