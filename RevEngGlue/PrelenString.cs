using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevEngGlue
{
    namespace PrelenString
    {
        // roughly base on 'prelen.bt' binary template
        // see here: https://gist.github.com/x1nixmzeng/3805536

        public class PLStringReader : IPLStringReader
        {
            BinReaderBase br;

            /// <summary>
            /// Property which defines how strings should be represented
            /// </summary>
            public Encoding Encoding { get; set; }

            /// <summary>
            /// Construct a new instance from a reader
            /// </summary>
            public PLStringReader(BinReaderBase reader)
            {
                br = reader;
                Encoding = Encoding.Default;
            }

            /// <summary>
            /// Construct a new instance from a reader and default encoding
            /// </summary>
            public PLStringReader(BinReaderBase reader, Encoding enc)
            {
                br = reader;
                Encoding = enc;
            }

            /// <summary>
            /// #private Generate the metadata required to internally manipulate a string
            /// </summary>
            private StringMeta GetMeta(PrelenStringParams p, List<byte> source)
            {
                StringMeta meta;

                meta.String = Encoding.GetString(source.ToArray());
                meta.StringLength = meta.String.Length;

                meta.StringSize = source.Count;

                // Add nul-terminator size to read string size
                if (p.length == Length.lNul)
                {
                    switch (p.size)
                    {
                        case Size.s8:
                            meta.StringSize += sizeof(byte);
                            break;
                        case Size.s16:
                            meta.StringSize += sizeof(short);
                            break;
                    }
                }

                return meta;
            }

            /// <summary>
            /// #private Copy a 16-bit character into a 8-bit list
            /// </summary>
            private static void ToByteList(List<byte> result, ushort wchar)
            {
                var bytes = BitConverter.GetBytes(wchar);

                result.Add(bytes[0]);
                result.Add(bytes[1]);
            }

            /// <summary>
            /// #private Copy an array of 16-bit characters into a 8-bit list
            /// </summary>
            private static void ToByteList(List<byte> result, ushort[] wstring)
            {
                foreach (ushort val in wstring)
                {
                    ToByteList(result, val);
                }
            }

            /// <summary>
            /// #private Extract a string using a specific set of parameters
            /// </summary>
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

                    case Size.s16:
                        {
                            if (p.length == Length.lNul)
                            {
                                for (ushort val = br.u16(); val != 0; val = br.u16())
                                {
                                    ToByteList(raw, val);
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

                                var val = br.u16((int)len);
                                ToByteList(raw, val);
                            }
                        }
                        break;
                }

                return GetMeta(p, raw);
            }

            /// <summary>
            /// Calculate the actual length of a string, given the encoding and metadata
            /// </summary>
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

            /// <summary>
            /// Read a null-terminated string of 8-bits
            /// </summary>
            public StringMeta cstr()
            {
                return Read(new PrelenStringParams(PrelenString.Size.s8, PrelenString.Length.lNul));
            }

            /// <summary>
            /// Read a null-terminated string of 16-bits
            /// </summary>
            public StringMeta wcstr()
            {
                return Read(new PrelenStringParams(PrelenString.Size.s16, PrelenString.Length.lNul));
            }

            /// <summary>
            /// Read a string of 8-bits with a known length
            /// </summary>
            public StringMeta str(int length)
            {
                return Read(new PrelenStringParams(PrelenString.Size.s8, length));
            }

            /// <summary>
            /// Read a string of 16-bits with a known length
            /// </summary>
            public StringMeta wstr(int length)
            {
                return Read(new PrelenStringParams(PrelenString.Size.s16, length));
            }

            /// <summary>
            /// Read a string using a specific set of parameters
            /// </summary>
            public StringMeta pl(PrelenStringParams param)
            {
                return Read(param);
            }
        }
    }
}
