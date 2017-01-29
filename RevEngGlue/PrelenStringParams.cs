namespace RevEngGlue
{
    namespace PrelenString
    {
        /// <summary>
        /// Definitions of supported string sizes
        /// </summary>
        public enum Size
        {
            s8,
            s16,
        }

        /// <summary>
        /// Definitions of supported string alignment
        /// </summary>
        public enum Align
        {
            a16,
            a32,
            aNone,
        }

        /// <summary>
        /// Definitions of supported string lengths, including leading fields
        /// </summary>
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

            /// <summary>
            /// Construct a new instance from a size
            /// </summary>
            public PrelenStringParams(Size s)
            {
                size = s;
                align = Align.aNone;
                length = Length.lNul;
                read = 0;
            }

            /// <summary>
            /// Construct a new instance from a size and fixed-length, with optional alignment
            /// </summary>
            public PrelenStringParams(Size s, int len, Align a = Align.aNone)
            {
                size = s;
                align = a;
                length = Length.lFixed;
                read = len;
            }

            /// <summary>
            /// Construct a new instance from a size and length, with optional alignment
            /// </summary>
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
