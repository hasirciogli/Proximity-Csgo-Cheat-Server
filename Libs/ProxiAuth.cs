using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogsoftwareServer.Libs
{
    internal class ProxiAuth
    {
        public ProxiAuth()
        {
            tbData[] tbbdAT =
            {
                new tbData("a", "010"),
                new tbData("b", "020"),
                new tbData("c", "030"),
                new tbData("d", "040"),
                new tbData("e", "050"),
                new tbData("f", "060"),
                new tbData("g", "070"),
                new tbData("h", "080"),
                new tbData("i", "090"),
                new tbData("j", "100"),
                new tbData("k", "110"),
                new tbData("l", "120"),
                new tbData("m", "130"),
                new tbData("n", "140"),
                new tbData("o", "150"),
                new tbData("p", "160"),
                new tbData("r", "170"),
                new tbData("s", "180"),
                new tbData("t", "190"),
                new tbData("u", "200"),
                new tbData("v", "210"),
                new tbData("y", "220"),
                new tbData("z", "230"),
                new tbData("x", "240"),
                new tbData("q", "250"),
                new tbData("w", "260"),
                new tbData("=", "270"),
                new tbData("_", "280"),
                new tbData("\\", "290"),
                new tbData("/", "300"),
                new tbData("&", "310"),
                new tbData("+", "320"),
                new tbData("0", "330"),
                new tbData("1", "340"),
                new tbData("2", "350"),
                new tbData("3", "360"),
                new tbData("4", "370"),
                new tbData("5", "380"),
                new tbData("6", "390"),
                new tbData("7", "400"),
                new tbData("8", "410"),
                new tbData("9", "420"),
                new tbData(" ", "430"),





                new tbData("A", "005"),
                new tbData("B", "015"),
                new tbData("C", "025"),
                new tbData("D", "035"),
                new tbData("E", "045"),
                new tbData("F", "055"),
                new tbData("G", "065"),
                new tbData("H", "075"),
                new tbData("I", "085"),
                new tbData("J", "095"),
                new tbData("K", "105"),
                new tbData("L", "115"),
                new tbData("M", "125"),
                new tbData("N", "135"),
                new tbData("O", "145"),
                new tbData("P", "155"),
                new tbData("R", "165"),
                new tbData("S", "175"),
                new tbData("T", "185"),
                new tbData("U", "195"),
                new tbData("V", "205"),
                new tbData("Y", "215"),
                new tbData("Z", "225"),
                new tbData("X", "235"),
                new tbData("Q", "245"),
                new tbData("W", "255")
            };

            this.tFiles.AddRange(tbbdAT);
        }

        int getRnd()
        {
            return new Random().Next(10, 100);
        }

        public struct tbData
        {
            public tbData(string one, string sec)
            {
                this.source = one;
                this.dest = sec;
            }

            public string source;
            public string dest;
        };

        public List<tbData> tFiles = new List<tbData>();

        public void EnCrypt(string iData, out string oData)
        {
            string tiData = iData;
            oData = "";
            oData += getRnd().ToString();

            for (int i = 0; i < tiData.Length; i++)
            {
                for (int i2 = 0; i2 < this.tFiles.Count(); i2++)
                {
                    if (this.tFiles[i2].source[0] == tiData[i])
                    {
                        oData += this.tFiles[i2].dest;
                        oData += this.getRnd();
                    }
                }
            }
        }
        void DeCrypt(string iData, out string oData)
        {
            oData = "";
            string tiData = iData;

            for (int i = 0; i < iData.Length; i++)
            {
                string cStr = "";

                if ((i % 5 == 2))
                {
                    cStr += iData[i];
                    cStr += iData[i + 1];
                    cStr += iData[i + 2];

                    for (int i2 = 0; i2 < tFiles.Count; i2++)
                    {
                        if (tFiles[i2].dest == cStr)
                        {
                            oData += tFiles[i2].source[0];
                        }
                    }
                }
            }
        }


        public bool AuthProxiClient(string proxiData, Client _cl)
        {

            DeCrypt(proxiData, out string o1);

            string let = o1;

            DeCrypt(let, out string o2);

            int wLen = Convert.ToInt32(o2.Substring(0, 4));
            string proxiKey = o2.Substring(4, wLen);
            string algorithmKey = o2.Substring(wLen+4, o2.Length - wLen - 4);

            if (proxiKey == "andjg2hwken8sndensqe3sjmd7fdhwjewmaoe6jakafs9df4ghjklglm0bvnmxndskjf")
            {
                string keyResJonkermantekentaJUK = "ok" + algorithmKey + proxiKey;

                this.EnCrypt(keyResJonkermantekentaJUK, out string o11);
                this.EnCrypt(o11, out string o12);

                _cl.sendBuffers.Add(o12);
                return true;
            }
            else
            {
                return false;
            }

            
        }
    }
}