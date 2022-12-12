using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogsoftwareServer.packet.handlers
{
    public class CheatPacketHandler
    {
        public string data;

        public CheatPacketHandler(string data) 
        {
            this.data = data;
        }

        public bool Handle()
        {
            JObject obj;
            string packetID;
            try 
            {
                obj         = JObject.Parse(data);
                packetID    = obj.SelectToken("packet_id").ToString();
            }
            catch (Exception ex)
            {

                return false;
            }

            switch (packetID)
            {
                default:
                    break;
            }

            return true;
        }
    }
}
