using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tele
{
    public class Crc : CodeBase
    {
        public static readonly long ATM = 0x107;
        public static readonly long CRC12 = 0x180F;
        public static readonly long CRC16 = 0x18005;
        public static readonly long CRC16_REVERSE = 0x14003;
        public static readonly long SDLC = 0x11021;
        public static readonly long SDLC_REVERSE = 0x10811;
        public static readonly long CRC32 = 0x104C11DB7L;

        protected long key = 0x18005;
        protected int keyLength = 16;

        public void setKey(long _key)
        {
            this.key = _key;

            if (_key == ATM)
            {
                keyLength = 8;
            }
            else if (_key == CRC12)
            {
                keyLength = 12;
            }
            else if (_key == CRC32)
            {
                keyLength = 32;
            }
            else
            {
                keyLength = 16;
            }
        }

        private int xor(int a, int b)
        {
            if(a==b)
            {
                return 0; 
            }else
            {
                return 1;
            }
        }

        private int[] CountCrc(int[] bity)
        {
            int n = bity.Length;
            int[] crc = new int[keyLength];
            int[] temp = new int[n + keyLength];
            Array.Copy(bity, 0, temp, keyLength, n);
            int[] _key = new int[keyLength + 1];
            for(int i=0; i < keyLength+1; i++)
            {
                if((key&(1<<i))==0)
                {
                    _key[i] = 0;
                }
                else
                {
                    _key[i] = 1;
                }
            }

            for(int start = n+keyLength-1; start > keyLength-1; start--)
            {
                if(temp[start]==1)
                {
                    for (int i = 0; i < keyLength + 1; i++)
                    {
                        temp[start - i] = xor(temp[start - i], _key[keyLength - i]);
                    }
                }
            }

            Array.Copy(temp, 0, crc, 0, keyLength);
            return crc;
        }

        public override int[] decode()
        {
            int l = code.Length;
            int n = l - keyLength;
            data = new int[n];
            for(int i=0; i < n; i++)
            {
                data[i] = code[i + keyLength];
            }

            return data;
        }

        public override int[] encode()
        {
            int n = data.Length;
            int l = n + keyLength;
            code = new int[l];
            type = new int[l];
            Array.Copy(data, 0, code, keyLength, n);
            int[] crc = CountCrc(data);
            Array.Copy(crc, 0, code, 0, keyLength);
            for(int i=0; i < keyLength; i++)
            {
                type[i] = 3;
            }
            for(int i=keyLength; i < l; i++)
            {
                type[i] = 0;
            }

            return code;
        }

        public override void fix()
        {
            int l = code.Length;
            type = new int[l];
            int[] crc = CountCrc(code);
            bool ok = true;
            for(int i=0; i < keyLength && ok; i++)
            {
                if(crc[i] != 0)
                {
                    ok = false;
                }
            }
            if(ok)
            {
                for (int i = 0; i < keyLength; i++)
                {
                    type[i] = 3;
                }
                for(int i = keyLength; i < l; i++)
                {
                    type[i] = 0;
                }
            }
            else
            {
                errors++;
                for (int i = 0; i < keyLength; i++)
                {
                    type[i] = 5;
                }
                for (int i = keyLength; i < l; i++)
                {
                    type[i] = 2;
                }
            }
        }
    }
}
