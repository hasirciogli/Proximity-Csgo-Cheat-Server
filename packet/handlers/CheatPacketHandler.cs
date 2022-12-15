using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PacketEnums;

namespace RogsoftwareServer.packet.handlers
{
    public class CheatPacketHandler
    {
        public byte[] cphData = new byte[8192];
        public Client client;

        public CheatPacketHandler(Client _cl, byte[] data) 
        {
            this.cphData = data;
            this.client    = _cl;
        }

        public bool Handle()
        {


            try
            {

                string bbgjk = Encoding.UTF8.GetString(this.cphData);


                JObject obj = new JObject();
                int packetID;
                obj = JObject.Parse(bbgjk);
                packetID = Convert.ToInt32(obj.SelectToken("packet_id"));


                switch ((PacketEnums.CHEAT.ClientToServer)packetID)
                {
                    case PacketEnums.CHEAT.ClientToServer.USER_AUTH:
                        if (!new workers.CheatWorker.fromClientToServer().UserAuth(this.client, this.cphData))
                        {   

                        }

                        break;
                    case PacketEnums.CHEAT.ClientToServer.CHAT_MESSAGE_SENT:
                        if (!new workers.CheatWorker.fromClientToServer().ChatMessageSent(this.client, this.cphData))
                        {

                        }

                        break;
                }

            }
            catch (Exception ex)
            {
                if (Globals.loggerConfig.isDebugMode)
                    Globals.LoggerG.Log("handle object exception");
                //return false;
            }

            Globals.LoggerG.Log("data is -> " + Encoding.UTF8.GetString(this.cphData));

            return true;
        }
    }
}
