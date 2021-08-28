using System;
using System.Threading.Tasks;

namespace tele
{
    public abstract class CodeBase
    {
        protected int[] data;
        protected int[] code;
        protected int[] type; 
        protected int errors=0;
        protected int[] inputData;

        public void setData(int[] _data)
        {
            this.data = new int[_data.Length];
            Array.Copy(_data,0, this.data, 0,_data.Length);
            Array.Copy(_data, 0, this.inputData, 0, _data.Length);
            errors = 0;
        }

        public void setData(Bits d)
        {
            int[] b = d.getBits();
            this.data = new int[b.Length];
            Array.Copy(b, 0, this.data, 0, b.Length);
            Array.Copy(b, 0, this.inputData, 0, b.Length);
            errors = 0;
        }

        public void setData(string str)
        {
            int n = str.Length;
            data = new int[n];
            inputData = new int[n];
            for(int i=0; i < n; i++)
            {
                if(str[i] == '1')
                {
                    data[i] = 1;
                    inputData[i] = 1;
                }else{
                    data[i] = 0;
                    inputData[i] = 0;
                }
            }
            errors = 0;
        }

        public void setCode(int[] _code)
        {
            this.code = new int[_code.Length];
            this.type = new int[_code.Length];
            Array.Copy(_code, 0, this.code, 0, _code.Length);
        }

        public void setCode(Bits d)
        {
            int[] b = d.getBits();
            this.code = new int[b.Length];
            this.type = new int[b.Length];
            Array.Copy(b, 0, this.code, 0, b.Length);
        }

        public void setCode(string str)
        {
            int n = str.Length;
            code = new int[n];
            this.type = new int[n];
            for(int i=0; i < n; i++)
            {
                if(str[i] == '1')
                {
                    code[i] = 1;
                }else{
                    code[i] = 0;
                }
            }
            errors = 0;
        }

        public string getInputData()
        {
            Bits d = new Bits();
            d.fromInt(inputData);
            return d.toString();
        }

        public abstract int[] encode();
        public abstract int[] decode();

        public int getDataBitsNumber()
        {
            return data.Length;
        }

        public int getControlBitsNumber()
        {
            return code.Length - data.Length;
        }

        public int getDetectedErrorsNumber()
        {
            return errors;
        }

        public int getFixedErrorsNumber()
        {
            int b=0;
            for(int i=0; i < type.Length; i++)
            {
                if(type[i] == 1 || type[i] == 4)
                {
                    b++;
                }
            }
            return b;
        }

        public string codeToString()
        {
            Bits d = new Bits();
            d.fromInt(code);
            return d.toString();
        }

        public string dataToString()
        {
            Bits d = new Bits();
            d.fromInt(data);
            return d.toString();
        }

        public int[] getBitTypes()
        {
            return type;
        }

        public abstract void fix();

        public void interfere(int n)
        {
            int l = code.Length;
            Random gen = new Random();
            if(n>l)
            {
                n=l;
            }
            int position;
            int interfered=0;
            for(int i=0; i < l; i++)
            {
                type[i] = 0;
            }
            while(interfered < n)
            {
                position = gen.Next(l);
                if(type[position]==0)
                {
                    if(code[position]==1)
                    {
                        code[position]=0;
                    }else{
                        code[position]=1;
                        interfered++;
                    }
                }
            }
        }

        public void negate(int position)
        {
            if(position < code.Length)
            {
                if(code[position] == 1)
                {
                    code[position] = 0;
                }else{
                    code[position] = 1;
                }
                if(type[position]==1)
                {
                    type[position]=0;
                }else{
                    type[position]=1;
                }
            }
        }

        public int countErrors(string _input, string _output)
        {
            string input = _input;
            string output = _output;
            if (input.Length != output.Length)
            {
                return -1;
            }
            else
            {
                errors = 0;
                int l = input.Length;
                for (int i = 0; i < l; i++)
                {
                    if (input[i] != output[i])
                    {
                        errors++;
                    }
                }
                 return errors;
            }
        }
    }
}
