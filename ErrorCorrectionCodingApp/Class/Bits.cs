using System;

namespace tele
{
    public class Bits
    {
        protected int n;
        protected int[] bits;

        public void generate(int n)
        {
            this.n = n;
            bits = new int[n];
            Random gen = new Random();
            for(int i=0; i < n; i++)
            {
                bits[i] = gen.Next(2);
            }
        }


        public string toString()
        {
            string str = "";
            for (int i=0; i < n; i++) 
            {
                str+= bits[i];
            }

            return str;
        }

        public void fromString(string str) 
        {
           n = str.Length;
           bits = new int[n];
           for(int i=0; i < n; i++) 
           {
               if(str[i] == '1') 
               {
                   bits[i] = 1;
               }else{
                   bits[i] = 0;
               }
           } 
        }

        public void fromInt(int[] bity)
        {
            this.n = bity.Length;
            this.bits = new int[n];
            this.bits = new int[n];
            Array.Copy(bity, 0, this.bits, 0, bity.Length);
        }

        public int[] getBits()
        {
            return bits;
        }

        public string getBitsAsString()
        {
            string str = "";
            for(int i=0; i<bits.Length;i++)
            {
                str += bits[i].ToString();
            }

            return str;
        }
    }
}
