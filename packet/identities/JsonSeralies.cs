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
                public string hwid;
            }
        }

        namespace serverToClient
        {
            public class UserAuth
            {
                public class thisZData
                {
                    public bool isSuccess = false;
                    public string token = "";
                    public string username = "";
                    public string subs_till = "";
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }

            public class CHAT_MESSAGE_SENT
            {
                public class thizCData
                {
                    public thizCData(float x, float y, float z)
                    {
                        this.x = x;
                        this.y = y;
                        this.z = z;
                    }
                    public thizCData()
                    {

                    }
                    public float x, y, z;
                }
                public class thisZData
                {
                    public int message_id;
                    public string message_content         = "";
                    public thizCData message_content_color;
                    public string message_author          = "";
                    public thizCData message_author_color;
                    public string message_date            = "";
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }

            public class NEED_AUTH
            {
                public class thisZData  
                {

                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }
        }
    }
}
