using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tele
{
    public class Hamming : CodeBase
    {
        public override int[] decode()
        {
            int n = code.Length;
            int d = 0;
            int overloadBits = 0;
            for(int i=0; i < n; i++)
            {
                if(Math.Pow(2, overloadBits)-1 != i)
                {
                    d++;
                }
                else
                {
                    overloadBits++;
                }  
            }

            data = new int[d];
            d = 0;
            overloadBits = 0;

            for (int i = 0; i < n; i++)
            {
                if (Math.Pow(2, overloadBits) - 1 != i)
                {
                    data[d] = code[i];
                    d++;
                }
                else
                {
                    overloadBits++;
                }
            }
            return data;
        }

        public override int[] encode()
        {
            int n = data.Length;
            int i = 0;
            int overloadBits = 0;
            int encodedLen = 0;
            while(i < n)
            {
                if(Math.Pow(2,overloadBits)-1 == encodedLen)
                {
                    overloadBits++;
                }
                else
                {
                    i++;
                }
                encodedLen++;
            }
            code = new int[encodedLen];
            type = new int[encodedLen];

            long mask = 0;
            overloadBits = 0;
            int d = 0;
            i = 0;
            encodedLen = 0;
            while(i < n)
            {
                if(Math.Pow(2, overloadBits)-1 == encodedLen)
                {
                    overloadBits++;
                }
                else
                {
                    code[encodedLen] = data[i];
                    if(data[i] == 1)
                    {
                        mask ^= encodedLen + 1;
                    }
                    i++;
                }
                encodedLen++;
            }

            overloadBits = 0;
            for(i=0; i < encodedLen; i++)
            {
                if(Math.Pow(2, overloadBits)-1 == i)
                {
                    if((mask&((long)1<<overloadBits)) == 0)
                    {
                        code[i] = 0;
                    }
                    else
                    {
                        code[i] = 1;
                    }
                    overloadBits++;
                }
            }

            return code;
        }

        public override void fix()
        {
            int n = code.Length;
            int d = 0;
            int overloadBits = 0;
            for(int i = 0; i < n; i++)
            {
                if(Math.Pow(2, overloadBits)-1 != i)
                {
                    d++;
                }
                else
                {
                    overloadBits++;
                }
            }

            data = new int[d];
            int mask = 0;
            d = 0;
            overloadBits = 0;

            for(int i =0; i < n; i++)
            {
                if(code[i] == 1)
                {
                    mask ^= i + 1;
                }

                if(Math.Pow(2, overloadBits)-1 != i)
                {
                    d++;
                    type[i] = 0;
                }
                else
                {
                    type[i] = 3;
                    overloadBits++;
                }
            }

            if(mask != 0)
            {
                errors++;
                int position = mask - 1;

                if(position < code.Length)
                {
                    if(type[position] == 0)
                    {
                        type[position] = 1;
                    }else if(type[position] == 3)
                    {
                        type[position] = 4;
                    }

                    if(code[position] == 1)
                    {
                        code[position] = 0;
                    }
                    else
                    {
                        code[position] = 1;
                    }
                }
            }
        }
    }
}
