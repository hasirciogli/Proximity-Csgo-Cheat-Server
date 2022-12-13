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

        namespace serverToClient
        {
            public class ServerFirstAuthPacketData
            {
                
            }

            public class ServerLoginPacketData
            {
                public class thisZData
                {
                    public bool isSuccess = false;
                    public string token = "";
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }
            
        }
    }
}
