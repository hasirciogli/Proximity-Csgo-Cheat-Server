using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
            public class FUN_OK
            {
                public int packet_id = 0;
            }
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

            public class CONFIG_CREATE
            {
                public class thisZData
                {
                    public int config_id = -2;
                    public string config_author = "";
                    public string config_name = "";
                    public string config_date = "";
                }

                public int packet_id = 0;
                public bool status = false;
                public thisZData data = new thisZData();
            }

            public class CONFIG_LOAD
            {
                public class thisZData
                {
                    public int config_id = -2;
                    public JObject config_data;
                }

                public int packet_id = 0;
                public bool status = false;
                public thisZData data = new thisZData();
            }

            public class CONFIG_REFRESH
            {
                public class thisZData
                {
                    public int config_id = -2;
                    public string config_author = "";
                    public string config_name = "";
                    public string config_date = "";
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

            public class VERSION_RESP
            {
                public class thisZData
                {
                    public bool valid = false;
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }
        }
    }



    namespace LoaderPacketData
    {
        namespace serverToClient
        {
            public class FUN_OK
            {
                public int packet_id = 0;
            }

            public class USER_AUTH
            {
                public class thisZData
                {
                    public bool   isSuccess        = false;
                    public bool   isBanned         = false;
                    public bool   subs_active      = false;
                    public bool   need_hwid_reset  = false;
                    public string uBanReason       = "";
                    public string username         = "";
                    public string subs_till        = "";
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }

            public class HWID_AUTH
            {
                public class thisZData
                {
                    public bool isSuccess = false;
                    public bool subs_active = false;
                    public bool   isBanned         = false;
                    public string username = "";
                    public string uBanReason = "";
                    public string subs_till = "";
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }
            
            public class VERSION_RESP
            {
                public class thisZData
                {
                    public bool valid = false;
                }

                public int packet_id = 0;
                public thisZData data = new thisZData();
            }
        }
    }

}
