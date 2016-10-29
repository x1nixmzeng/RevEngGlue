using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevEngGlue
{
    namespace PrelenString
    {
        interface IPLStringReader
        {
            string cstr();
            string wcstr();

            string str(int length);
            string wstr(int length);
        }
    }
}
