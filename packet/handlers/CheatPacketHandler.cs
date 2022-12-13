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
        public string data;
        public Client client;

        public CheatPacketHandler(Client _cl, string data) 
        {
            this.data      = data;
            this.client    = _cl;
        }

        public bool Handle()
        {
            JObject obj;
            int packetID;

            try 
            {
                obj         = JObject.Parse(this.data);
                packetID    = Convert.ToInt32(obj.SelectToken("packet_id"));
            }
            catch (Exception ex) 
            {
                return false;
            }

            switch ((PacketEnums.CHEAT.ClientToServer)packetID)
            {
                case PacketEnums.CHEAT.ClientToServer.USER_AUTH:
                    if (new workers.CheatWorker.fromClientToServer().UserAuth(this.client, this.data))
                    {

                    }
                    break;
            }

            return true;
        }
    }
}
