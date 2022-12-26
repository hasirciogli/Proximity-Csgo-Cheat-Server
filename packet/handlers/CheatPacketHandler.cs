using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            this.client = _cl;
        }

        public bool HandleCheat()
        {
            try
            {
                string bbgjk = Encoding.UTF8.GetString(this.cphData);

                JObject obj = new JObject();
                int packetID;

                try
                {
                    obj = JObject.Parse(bbgjk);
                }
                catch (JsonException e)
                {
                    //Globals.LoggerG.Log(e.ToString());
                    return false;
                }
                
                packetID = Convert.ToInt32(obj.SelectToken("packet_id"));

                if ((!this.client.CConfig.userAuthed || this.client.CConfig.userToken == "") && packetID != (int)PacketEnums.CHEAT.ClientToServer.USER_AUTH)
                {
                    new workers.CheatWorker.fromServerToClient().SendNeedAuth(this.client);
                    return true;
                }

                switch ((PacketEnums.CHEAT.ClientToServer)packetID)
                {
                    case PacketEnums.CHEAT.ClientToServer.USER_AUTH:
                        bool uAResponse = new workers.CheatWorker.fromClientToServer().UserAuth(this.client, this.cphData);

                        if (uAResponse)
                        {
                            Globals.addNewUser(this.client.clientID, this.client.CConfig.userID.ToString(), this.client.CConfig.username, this.client.CConfig.steamID, this.client.CConfig.steamName);
                        }
                        else
                            return false;

                        break;


                    case PacketEnums.CHEAT.ClientToServer.CHAT_MESSAGE_SENT:
                        bool cMSResponse = new workers.CheatWorker.fromClientToServer().ChatMessageSent(this.client, this.cphData);
                        if (cMSResponse)
                        {

                        }
                        else
                            return false;
                        break;




                    default:
                        return false;
                        break;
                }

            }
            catch (Exception ex)
            {
                //if (Globals.loggerConfig.isDebugMode)
                //    Globals.LoggerG.Log("handle object exception");

                return false;
            }
            return true;
        }

        public bool HandleLoader()
        {
            try
            {
                string bbgjk = Encoding.UTF8.GetString(this.cphData);

                JObject obj = new JObject();
                int packetID;

                try
                {
                    obj = JObject.Parse(bbgjk);
                }
                catch (JsonException e)
                {
                    //Globals.LoggerG.Log(e.ToString());
                    return false;
                }

                packetID = Convert.ToInt32(obj.SelectToken("packet_id"));

                //switch ((PacketEnums.LOADER.ClientToServer)packetID)
                //{
                //    case PacketEnums.CHEAT.ClientToServer.USER_AUTH:
                //        if (!new workers.CheatWorker.fromClientToServer().UserAuth(this.client, this.cphData))
                //        {

                //        }

                //        break;
                //    case PacketEnums.CHEAT.ClientToServer.CHAT_MESSAGE_SENT:
                //        if (!new workers.CheatWorker.fromClientToServer().ChatMessageSent(this.client, this.cphData))
                //        {

                //        }
                //        break;
                //    default:
                //        return false;
                //        break;
                //}

            }
            catch (Exception ex)
            {
                //if (Globals.loggerConfig.isDebugMode)
                //      Globals.LoggerG.Log("handle object exception");

                return false;
            }
            return true;
        }

    }
}
