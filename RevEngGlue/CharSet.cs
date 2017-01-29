using System.Text;

namespace RevEngGlue
{
    // http://www.sweetscape.com/010editor/manual/FuncString.htm

    public class CharSet
    {
        public static Encoding ASCII
        {
            get
            {
                return Encoding.ASCII;
            }
        }

        public static Encoding ANSI
        {
            get
            {
                // https://msdn.microsoft.com/en-us/library/system.text.encoding(v=vs.110).aspx
                // "For ANSI encodings, the best fit behavior is the default."
                return Encoding.Default;
            }
        }

        public static Encoding Japanese
        {
            get
            {
                return Encoding.GetEncoding("shift_jis", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
            }
        }

        public static Encoding Unicode
        {
            get
            {
                return Encoding.Unicode;
            }
        }

        public static Encoding UTF8
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
