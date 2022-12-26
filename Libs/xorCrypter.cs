using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xorCrypter
{
    public class xorCrptr
    {
        public string crypto(string tData)
        {
            char[] key = { 'P', 'C', 'C' }; //Any chars will work, in an array of any size
            char[] output = new char[tData.Length];

            for (int i = 0; i < tData.Length; i++)
            {
                output[i] = (char)(tData[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }

        public static xorCrptr cfun()
        {
            return new xorCrptr();
        }
    }
}
