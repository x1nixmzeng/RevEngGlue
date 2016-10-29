using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevEngGlue
{
    namespace PrelenString
    {
        public enum Size
        {
            s8,
            s16,
        }

        public enum Align
        {
            a16,
            a32,
            aNone,
        }

        public enum Length
        {
            l8,
            l16,
            l32,
            lNul,
            lFixed,
        };

        public class PrelenStringParams
        {
            public Size size;
            public Align align;
            public Length length;
            public int read;

            public PrelenStringParams(Size s)
            {
                size = s;
                align = Align.aNone;
                length = Length.lNul;
                read = 0;
            }

            public PrelenStringParams(Size s, int len, Align a = Align.aNone)
            {
                size = s;
                align = a;
                length = Length.lFixed;
                read = len;
            }

            public PrelenStringParams(Size s, Length l, Align a = Align.aNone)
            {
                size = s;
                length = l;
                align = a;
                read = 0;
            }
        }
    }
}
