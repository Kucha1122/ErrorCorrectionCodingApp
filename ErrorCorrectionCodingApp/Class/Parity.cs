using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tele
{
    public class Parity : CodeBase
    {
        public override int[] decode()
        {
            int n = code.Length;
            int bytes = n / 9;
            data = new int[bytes * 8];
            int oneBit;
            errors = 0;
            
            for(int i=0; i < bytes; i++)
            {
                oneBit = 0;
                for(int j=0; j < 8; j++)
                {
                    data[i * 8 + j] = code[i * 9 + j + 1];
                    oneBit += code[i * 9 + j + 1];
                }
                oneBit += code[i * 9];

                if(oneBit % 2 == 0)
                {
                    type[i * 9] = 3;
                    for(int j=1; j < 9; j++)
                    {
                        type[i * 9 + j] = 0;
                    }
                }
                else
                {
                    errors++;
                    type[i * 9] = 5;
                    for(int j=1; j < 9; j++)
                    {
                        type[i * 9 + j] = 2;
                    }
                }
            }
            return data;
        }

        public override int[] encode()
        {
            int n = data.Length;
            int bytes = n / 8;
            n += bytes;
            code = new int[n];
            type = new int[n];
            int oneBit;

            for(int i=0; i < bytes; i++)
            {
                oneBit = 0;
                for(int j=0; j < 8; j++)
                {
                    code[i * 9 + j + 1] = data[i * 8 + j];
                    oneBit += data[i * 8 + j];
                }
                if(oneBit % 2 == 1)
                {
                    code[i * 9] = 1;
                }
                else
                {
                    code[i * 9] = 0;
                }
            }
            return code;
        }

        public override void fix()
        {
            int n = code.Length;
            type = new int[n];
            int bytes = n / 9;
            errors = 0;

            int oneBit;
            for(int i=0; i < bytes; i++)
            {
                oneBit = 0;
                for (int j = 0; j < 8; j++)
                {
                    oneBit += code[i * 9 + j + 1];
                }
                oneBit += code[i * 9];
                if(oneBit % 2 == 0)
                {
                    type[i * 9] = 3;
                    for(int j=1; j < 9; j++)
                    {
                        type[i * 9 + j] = 0;
                    }
                }
                else
                {
                    errors++;
                    type[i * 9] = 5;
                    for(int j=1; j < 9; j++)
                    {
                        type[i * 9 + j] = 2;
                    }
                }
            }
        }
    }
}
