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

        public class PLStringReader : IPLStringReader
        {
            BinReaderBase br;

            public Encoding Encoding { get; set; }

            public PLStringReader(BinReaderBase reader)
            {
                br = reader;
                Encoding = Encoding.Default;
            }

            public string Read(PrelenStringParams p)
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
                                if( p.length == Length.lFixed )
                                {
                                    //raw = br.chunk8(p.read).ToList();
                                }
                                else
                                {
                                    uint len = 0;

                                    switch(p.length)
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

                                    raw = br.block((int)len).ToList();
                                }
                            }
                        }
                        break;
                }

                return Encoding.GetString(raw.ToArray());
            }

            public string cstr()
            {
                return Read(new PrelenStringParams(PrelenString.Size.s8, PrelenString.Length.lNul));
            }

            public string wcstr()
            {
                return Read(new PrelenStringParams(PrelenString.Size.s16, PrelenString.Length.lNul));
            }

            public string str(int length)
            {
                return Read(new PrelenStringParams(PrelenString.Size.s8, length));
            }

            public string wstr(int length)
            {
                return Read(new PrelenStringParams(PrelenString.Size.s16, length));
            }
        }
    }
}
