using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevEngGlue
{
    namespace PrelenString
    {
        // roughly base on 'prelen.bt' binary template
        // see here: https://gist.github.com/x1nixmzeng/3805536

        public class PLStringReader : IPLStringReaderBase
        {
            BinReaderBase br;

            public Encoding Encoding { get; set; }

            public PLStringReader(BinReaderBase reader)
            {
                br = reader;
                Encoding = Encoding.Default;
            }

            private StringMeta GetMeta(List<byte> source)
            {
                StringMeta meta;

                meta.String = Encoding.GetString(source.ToArray());
                meta.StringSize = source.Count;
                meta.StringLength = meta.String.Length;

                return meta;
            }

            private StringMeta Read(PrelenStringParams p)
            {
                var raw = new List<byte>();

                switch (p.size)
                {
                    case Size.s8:
                        {
                            if (p.length == Length.lNul)
                            {
                                for (byte val = br.u8(); val != 0; val = br.u8())
                                {
                                    raw.Add(val);
                                }
                            }
                            else
                            {
                                uint len = 0;

                                if (p.length == Length.lFixed)
                                {
                                    len = (uint)p.read;
                                }
                                else
                                {
                                    switch (p.length)
                                    {
                                        case Length.l8:
                                            {
                                                len = br.u8();
                                                break;
                                            }
                                        case Length.l16:
                                            {
                                                len = br.u16();
                                                break;
                                            }
                                        case Length.l32:
                                            {
                                                len = br.u32();
                                                break;
                                            }
                                    }
                                }

                                raw = br.u8((int)len).ToList();
                            }
                        }
                        break;
                }

                return GetMeta(raw);
            }

            public int StringLength(PrelenStringParams p, string source_string)
            {
                int len = Encoding.GetByteCount(source_string);

                if (p.length == Length.lNul)
                {
                    len += 1;
                }

                // TODO: alignment

                return len;
            }

            public StringMeta cstr()
            {
                return Read(new PrelenStringParams(PrelenString.Size.s8, PrelenString.Length.lNul));
            }

            public StringMeta wcstr()
            {
                return Read(new PrelenStringParams(PrelenString.Size.s16, PrelenString.Length.lNul));
            }

            public StringMeta str(int length)
            {
                return Read(new PrelenStringParams(PrelenString.Size.s8, length));
            }

            public StringMeta wstr(int length)
            {
                return Read(new PrelenStringParams(PrelenString.Size.s16, length));
            }
        }
    }
}
