using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PacketJsonSerializes
{
    namespace CheatPacketData
    {
        namespace clientToServer
        {
            public class CheatFirstAuthPacketData
            {
                public string machine_auth_key;
            }

            public class CheatLoginPacketData
            {
                public string userHWID;
            }
        }
    }
}
