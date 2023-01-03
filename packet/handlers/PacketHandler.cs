using Newtonsoft.Json.Linq;
using System;
using System.Text;
using Newtonsoft.Json;

namespace RogsoftwareServer.packet.handlers
{
    public class PacketHandler
    {
        public byte[] cphData = new byte[8192];
        public Client client;

        public PacketHandler(Client _cl, byte[] data)
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

                    case PacketEnums.CHEAT.ClientToServer.CONFIG_CREATE:
                        bool cCResponse = new workers.CheatWorker.fromClientToServer().ConfigCreate(this.client, this.cphData);
                        if (cCResponse)
                        {

                        }
                        else
                            return false;
                        break;


                    case PacketEnums.CHEAT.ClientToServer.CONFIG_REFRESH:
                        bool cRResponse = new workers.CheatWorker.fromClientToServer().ConfigRefresh(this.client, this.cphData);
                        if (cRResponse)
                        {

                        }
                        else
                            return false;
                        break;

                    case PacketEnums.CHEAT.ClientToServer.CONFIG_LOAD:
                        bool cLResponse = new workers.CheatWorker.fromClientToServer().ConfigLoad(this.client, this.cphData);
                        if (cLResponse)
                        {

                        }
                        else
                            return false;
                        break;

                    case PacketEnums.CHEAT.ClientToServer.CONFIG_SAVE:
                        bool cSResponse = new workers.CheatWorker.fromClientToServer().ConfigSave(this.client, this.cphData);
                        if (cSResponse)
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

                switch ((PacketEnums.LOADER.ClientToServer)packetID)
                {
                    case PacketEnums.LOADER.ClientToServer.USER_AUTH:
                        if (!new workers.LoaderWorker.fromClientToServer().UserAuth(this.client, this.cphData))
                        {

                        }

                        break;
                    case PacketEnums.LOADER.ClientToServer.HWID_AUTH:
                        if (!new workers.LoaderWorker.fromClientToServer().HwidAuth(this.client, this.cphData))
                        {

                        }
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

    }
}
